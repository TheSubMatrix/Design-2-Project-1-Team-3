using System;
using UnityEngine;
[Serializable]
public abstract class StaffSpell : ScriptableObject
{
    public abstract void Initialize();
    public abstract void ExecuteSpell(Vector3 position, Vector3 direction, Quaternion rotation);
    
}
