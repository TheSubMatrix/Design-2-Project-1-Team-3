using AudioSystem;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePickup : MonoBehaviour
{
	[SerializeField] protected float m_rotationSpeed = 100;
	[SerializeField] protected float m_bobbingSpeed = 1;
	[SerializeField] protected float m_bobbingAmount = 0.25f;
	[SerializeField] protected SoundData m_pickupSound;
	public UnityEvent OnPickup;
	protected virtual void OnDisable()
	{
		if (this.IsBeingUnloaded()) return;
		SoundManager.Instance?.CreateSound()?.WithSoundData(m_pickupSound).WithPosition(transform.position).WithRandomPitch().Play();
		OnPickup.Invoke();
	}
	protected virtual void Update()
	{
		transform.Rotate(Vector3.up, m_rotationSpeed * Time.deltaTime);
		transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time * m_bobbingSpeed) * Time.deltaTime * m_bobbingAmount), transform.position.z);
	}
}
