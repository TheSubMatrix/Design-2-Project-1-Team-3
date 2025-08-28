using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public abstract class StaffSpellSO : ScriptableObject, IEquatable<StaffSpellSO>
{
    [field: FormerlySerializedAs("<AttackName>k__BackingField")] [field:SerializeField]public string SpellName{ get; private set;}
    public abstract void Initialize();
    [field:SerializeField][ColorUsage(false, true)]public Color SpellBallColor { get; private set; }
    public abstract void ExecuteAttack(Vector3 position, Vector3 direction, Quaternion rotation);

    public bool Equals(StaffSpellSO other)
    {
        return other != null && SpellName == other.SpellName;
    }
    
}
