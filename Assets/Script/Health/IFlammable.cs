using UnityEngine;

public interface IFlammable
{
    public MonoBehaviour CurrentMonoBehaviour { get; }
    public IDamageable CurrentDamageable { get; }
}
