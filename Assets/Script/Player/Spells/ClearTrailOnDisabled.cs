using System;
using UnityEngine;

public class ClearTrailOnDisabled : MonoBehaviour
{
    TrailRenderer m_trailRenderer;
    void Awake()
    {
        m_trailRenderer = GetComponent<TrailRenderer>();
    }

    void OnDisable()
    {
        m_trailRenderer?.Clear();
    }
}
