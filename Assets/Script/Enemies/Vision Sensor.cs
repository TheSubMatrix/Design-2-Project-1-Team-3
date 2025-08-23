using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(SphereCollider))]
public class VisionSensor : MonoBehaviour
{
    [SerializeField] float m_sightRadius;
    SphereCollider m_sightSphere;
    [SerializeField]LayerMask m_layerMask;
    
    public GameObject m_selectedTarget;
    List<GameObject> m_visibleObjects = new();

#if UNITY_EDITOR
    [SerializeField] bool m_showGizmos = true;
    void OnValidate()
    {
        EditorApplication.delayCall += ()=>
        {
            if (this == null || (TryGetComponent(out SphereCollider setCollider) && setCollider == m_sightSphere)) return;
            m_sightSphere = setCollider;
            EditorUtility.SetDirty(this);
        };
        if(m_sightSphere is null) return;
        m_sightSphere.radius = m_sightRadius;
        m_sightSphere.isTrigger = true;
        m_sightSphere.includeLayers = m_layerMask;
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
        m_sightSphere.includeLayers = m_layerMask;
    }
    void OnTriggerEnter(Collider other)
    {
        if (m_visibleObjects.Contains(other.gameObject)) return;
        m_visibleObjects.Add(other.gameObject);
        UpdateSelectedTarget();
    }
    void OnTriggerExit(Collider other)
    {
        if (!m_visibleObjects.Contains(other.gameObject)) return;
        m_visibleObjects.Remove(other.gameObject);
        UpdateSelectedTarget();
    }

    void UpdateSelectedTarget()
    {
        if (m_visibleObjects.Count <= 0)
        {
            m_selectedTarget = null;
            return;
        }
        float bestDistance = Mathf.Infinity;
        GameObject bestTarget = null;
        foreach (GameObject currentObject in m_visibleObjects)
        {
            if (!(Vector3.Distance(currentObject.transform.position, transform.position) < bestDistance)) continue;
            bestDistance = Vector3.Distance(currentObject.transform.position, transform.position);
            bestTarget = currentObject;
        }
        m_selectedTarget = bestTarget;
    }
}
