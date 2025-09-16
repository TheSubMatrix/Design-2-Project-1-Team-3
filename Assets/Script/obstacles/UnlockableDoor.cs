using System.Collections.Generic;
using UnityEngine;


public class UnlockableDoor : MonoBehaviour
{
	bool m_isUnlocked;
	static readonly int Open = Animator.StringToHash("Open");
	[SerializeField] List<Animator> m_animators = new();
	
	void OnTriggerEnter(Collider other)
	{
		if (m_isUnlocked)
		{
			OnOpenDoor();
		}
	}
	void OnOpenDoor()
	{
		foreach (Animator animator in m_animators)
		{
			animator.SetTrigger(Open);
		}
	}

	public void Unlock()
	{
		m_isUnlocked = true;
	}
}
