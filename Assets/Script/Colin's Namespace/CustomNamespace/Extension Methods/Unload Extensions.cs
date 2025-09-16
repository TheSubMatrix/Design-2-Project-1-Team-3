using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public static class UnloadExtensions
{
	static readonly HashSet<Scene> BeingUnloaded = new();
	static readonly HashSet<Object> BeingDestroyed = new();
	static bool isApplicationQuitting;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void Reset()
	{
#if UNITY_EDITOR
		BeingUnloaded.Clear();
		BeingDestroyed.Clear();
		isApplicationQuitting = false;
		Application.quitting -= OnApplicationQuit;
		EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

		static void OnPlayModeStateChanged(PlayModeStateChange stateChange) => isApplicationQuitting = stateChange is PlayModeStateChange.ExitingPlayMode;
#endif

		Application.quitting += OnApplicationQuit;
		return;
		static void OnApplicationQuit() => isApplicationQuitting = true;
		
	}

	// Use this instead of SceneManager.UnloadSceneAsync
	public static AsyncOperation UnloadAsync(this Scene scene)
	{
		BeingUnloaded.Add(scene);
		AsyncOperation op = SceneManager.UnloadSceneAsync(scene);
		op.GetAwaiter().OnCompleted(OnSceneUnloadCompleted);
		return op;

		void OnSceneUnloadCompleted() => BeingUnloaded.Remove(scene);
	}
	
	// Use this instead of Object.Destroy
	public static async void Destroy(this Object target)
	{
		try
		{
			BeingDestroyed.Add(target);
			Object.Destroy(target);
			await Awaitable.NextFrameAsync();
			BeingDestroyed.Remove(target);
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	// Then this can be used during OnDisable
	public static bool IsBeingUnloaded(this Object target) => target switch
	{
		Component component => IsBeingUnloaded(component),
		GameObject gameObject => IsBeingUnloaded(gameObject),
		_ => isApplicationQuitting || BeingDestroyed.Contains(target)
	};

	public static bool IsBeingUnloaded(this Component component) => isApplicationQuitting || BeingDestroyed.Contains(component) || (component && IsBeingUnloaded(component.gameObject));

	public static bool IsBeingUnloaded(this GameObject gameObject)
	{
		if(isApplicationQuitting || BeingDestroyed.Contains(gameObject))
		{
			return true;
		}
		
		if(!gameObject)
		{
			return false;
		}

		if(BeingUnloaded.Contains(gameObject.scene))
		{
			return true;
		}

		for(var parent = gameObject.transform.parent; parent; parent = parent.parent)
		{
			if(BeingDestroyed.Contains(parent.gameObject))
			{
				return true;
			}
		}

		return false;
	}
}