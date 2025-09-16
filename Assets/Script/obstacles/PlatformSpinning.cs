using UnityEngine;

public class PlatformSpinning : MonoBehaviour
{
    [SerializeField] float m_fanSpeed = 30f;
    [SerializeField] Rigidbody m_rigidbody;
    [SerializeField] float m_angle = 0f;
    Vector3 m_initialOffset;

    void Start()
    {
        if (!m_rigidbody) m_rigidbody = GetComponent<Rigidbody>();
        m_initialOffset = m_rigidbody.position - transform.position;
        if (m_initialOffset == Vector3.zero) m_initialOffset = Vector3.right; // ensure non-zero radius
    }

    void FixedUpdate()
    {
        m_angle += m_fanSpeed * Time.fixedDeltaTime;
        if (m_angle >= 360f) m_angle -= 360f;

        Quaternion rot = Quaternion.AngleAxis(m_angle, Vector3.up);
        Vector3 targetPos = transform.position + rot * m_initialOffset;
        m_rigidbody.MovePosition(targetPos);
        m_rigidbody.MoveRotation(rot);
    }
}
