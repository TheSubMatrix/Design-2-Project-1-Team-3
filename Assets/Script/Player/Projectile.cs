using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
/// <summary>
/// The base class for any projectile fired from the staff. This class is designed to use an <see cref="ObjectPool{T}"/> no minimize the creation and destruction of <see cref="GameObject"/> runtime
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] protected uint Damage = 10;
    /// <summary>
    /// The <see cref="Rigidbody"/> attached to this <see cref="Projectile"/>
    /// </summary>
    protected Rigidbody ProjectileRigidbody;
    /// <summary>
    /// The <see cref="ObjectPool{T}"/> that contains this <see cref="Projectile"/>
    /// </summary>
    protected ObjectPool<Projectile> Pool;
    Coroutine m_destroyAfterTime;
    /// <summary>
    /// The amount of time a <see cref="Projectile"/> will stay alive for
    /// </summary>
    [field:SerializeField]public float Lifetime { get; protected set; }= 2;
    /// <summary>
    /// A <see cref="UnityEvent"/> that is invoked when the <see cref="Projectile"/> is pulled from the <see cref="ObjectPool{T}"/>
    /// </summary>
    [field:SerializeField] public UnityEvent OnPullEvent { get; private set; } = new();
    /// <summary>
    /// A <see cref="UnityEvent"/> that is invoked after the <see cref="Projectile"/> is pulled from the <see cref="ObjectPool{T}"/> and is being initialized
    /// </summary>
    [field:SerializeField] public UnityEvent OnInitializeEvent { get; private set; } = new();
    /// <summary>
    /// A <see cref="UnityEvent"/> that is invoked when the <see cref="Projectile"/> is fired
    /// </summary>
    [field:SerializeField] public UnityEvent OnFireEvent { get; private set; } = new();
    /// <summary>
    /// A <see cref="UnityEvent"/> that is invoked when the <see cref="Projectile"/> is returned to the <see cref="ObjectPool{T}"/>
    /// </summary>
    [field:SerializeField] public UnityEvent OnReleaseEvent { get; private set; } = new();

    /// <summary>
    /// Actions to occur when a <see cref="Projectile"/> is pulled from the <see cref="ObjectPool{T}"/>
    /// </summary>
    /// <param name="pool">The <see cref="ObjectPool{T}"/> that this projectile is in</param>
    public virtual void OnPull(ObjectPool<Projectile> pool)
    {
        Pool = pool;
        OnPullEvent?.Invoke();
    }
    
    /// <summary>
    /// Initializes the <see cref="Projectile"/> with the given position, rotation
    /// </summary>
    /// <param name="position">The position at which the <see cref="Projectile"/> should be initialized</param>
    /// <param name="rotation">The rotation with which the <see cref="Projectile"/> should be initialized</param>
    public virtual void OnInitialize(Vector3 position, Quaternion rotation)
    {
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        OnInitializeEvent?.Invoke();
        ProjectileRigidbody.isKinematic = false;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Gives the <see cref="Projectile"/> the given momentum using its <see cref="Rigidbody"/>
    /// </summary>
    /// <param name="projectileVelocity">The amount of velocity to give to the <see cref="Rigidbody"/> of the <see cref="Projectile"/></param>
    public virtual void OnFire(Vector3 projectileVelocity)
    {
        ProjectileRigidbody.linearVelocity = Vector3.zero;
        ProjectileRigidbody.AddForce(projectileVelocity, ForceMode.Impulse);
        StartCoroutine(DestroyProjectileAfterTimeAsync());
        OnFireEvent?.Invoke();
    }

    /// <summary>
    /// The actions to occur when a given <see cref="Projectile"/> is released back to the <see cref="ObjectPool{T}"/>
    /// </summary>
    public virtual void OnRelease()
    {
        if (m_destroyAfterTime is not null)
        {
            StopCoroutine(m_destroyAfterTime);
        }
        ProjectileRigidbody.isKinematic = true;
        gameObject.SetActive(false);
        OnReleaseEvent?.Invoke();
    }

    /// <summary>
    /// Actions that occur when this <see cref="Projectile"/> collides with something
    /// </summary>
    /// <param name="collision">The data from the <see cref="Collision"/> that occured</param>
    protected virtual void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            contact.otherCollider.gameObject.GetComponent<IDamageable>()?.Damage(Damage);
        }
        Pool?.Release(this);
    }

    /// <summary>
    /// Actions that occur when the <see cref="Projectile"/> is first created
    /// </summary>
    protected virtual void Awake()
    {
        ProjectileRigidbody = GetComponent<Rigidbody>();    
    }

    /// <summary>
    /// A <see cref="Coroutine"/> that returns the <see cref="Projectile"/> to its <see cref="ObjectPool{T}"/> after its <see cref="Lifetime"/> has expired
    /// </summary>
    protected virtual IEnumerator DestroyProjectileAfterTimeAsync()
    {
        yield return new WaitForSeconds(Lifetime);
        if (Pool is not null)
        {
            Pool.Release(this);   
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
