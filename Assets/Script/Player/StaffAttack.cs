using System;
using UnityEngine;
[Serializable]
public abstract class StaffAttack : ScriptableObject
{
    public abstract void Initialize();
    public abstract void ExecuteAttack(Vector3 position, Vector3 direction, Quaternion rotation);
    
}
