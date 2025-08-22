using System;
using System.Collections.Generic;
using CustomNamespace.Extensions;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class VisionSensor : MonoBehaviour
{
    [SerializeField] float m_sightRadius;
    SphereCollider m_sightSphere;
    [SerializeField]LayerMask m_layerMask;
    public List<GameObject> VisibleObjects { get; private set; } = new();

#if UNITY_EDITOR
    [SerializeField] bool m_showGizmos = true;
    void OnValidate()
    {
        EditorApplication.delayCall += ()=>
        {
            if (this == null || (TryGetComponent(out SphereCollider setCollider) && setCollider == m_sightSphere)) return;
            m_sightSphere = setCollider;
            UnityEditor.EditorUtility.SetDirty(this);
        };
        m_sightSphere.radius = m_sightRadius;
    }

    void OnDrawGizmos()
    {
        if(m_sightSphere is null || !m_showGizmos) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.TransformPoint(m_sightSphere.center), m_sightRadius);
    }
#endif

    void Awake()
    {
        m_sightSphere ??= GetComponent<SphereCollider>();
        m_sightSphere.radius = m_sightRadius;
        m_sightSphere.isTrigger = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (( m_layerMask & (1 << other.gameObject.layer)) != 0)
        {
            VisibleObjects.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (VisibleObjects.Contains(other.gameObject))
        {
            VisibleObjects.Remove(other.gameObject);
        }
    }
}
