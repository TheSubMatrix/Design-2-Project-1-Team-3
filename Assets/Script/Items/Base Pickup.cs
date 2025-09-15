using UnityEngine;
using UnityEngine.Events;

public abstract class BasePickup : MonoBehaviour
{
	public UnityEvent OnPickup;

	protected virtual void OnDestroy()
	{
		OnPickup.Invoke();
	}
}
