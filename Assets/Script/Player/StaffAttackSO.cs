using System;
using UnityEngine;
[Serializable]
public abstract class StaffAttackSO : ScriptableObject, IEquatable<StaffAttackSO>
{
    [field:SerializeField]public string AttackName{ get; private set;}
    public abstract void Initialize();
    public abstract void ExecuteAttack(Vector3 position, Vector3 direction, Quaternion rotation);

    public bool Equals(StaffAttackSO other)
    {
        return other != null && AttackName == other.AttackName;
    }
    
}
