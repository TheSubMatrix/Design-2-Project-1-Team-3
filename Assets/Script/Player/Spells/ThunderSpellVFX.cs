using UnityEngine;
using UnityEngine.VFX;
[RequireComponent(typeof(VisualEffect))]
public class ThunderSpellVFX : MonoBehaviour
{
    
    VFXEventAttribute m_boltDataToSend;
    VisualEffect m_vfx;
    public void Start()
    {
        m_vfx = GetComponent<VisualEffect>();
        m_boltDataToSend = m_vfx?.CreateVFXEventAttribute();
    }

    public struct  BoltPosition
    {
        public Vector3 StartPosition { get; }
        public Vector3 EndPosition { get; }
        public BoltPosition(Vector3 endPosition, Vector3 startPosition)
        {
            EndPosition = endPosition;
            StartPosition = startPosition;
        }
    }
    
    public void PlayVFX(BoltPosition[] boltPositions)
    {
        foreach (BoltPosition positions in boltPositions)
        {
            m_boltDataToSend.SetVector3("StartPosition", positions.StartPosition);
            m_boltDataToSend.SetVector3("endPoint", positions.EndPosition);
            m_vfx.SendEvent("Fire", m_boltDataToSend);
        }
    }

    public void OnDestroy()
    {
        m_boltDataToSend.Dispose();
    }
}
