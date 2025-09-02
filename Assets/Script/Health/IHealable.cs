using UnityEngine;

public interface IHealable
{
    public void Heal(uint amountToHeal);
    public abstract bool IsHealable { get; }
    public MonoBehaviour CurrentMonoBehaviour { get; }
}
