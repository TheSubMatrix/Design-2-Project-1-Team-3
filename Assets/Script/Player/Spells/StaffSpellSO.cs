using System;
using AudioSystem;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public abstract class StaffSpellSO : ScriptableObject, IEquatable<StaffSpellSO>
{
    [field: FormerlySerializedAs("<AttackName>k__BackingField")] [field:SerializeField]public string SpellName{ get; private set;}
    [field:SerializeField] public SoundData CastSound{ get; private set; }
    [field:SerializeField] public Sprite SpellSprite{ get; private set;}
    public abstract void Initialize();
    public virtual Color SpellBallColor { get; private set; }
    public abstract uint? UseCount { get; }
    public abstract void ExecuteAttack(Vector3 position, Vector3 direction, Quaternion rotation);
    public bool Equals(StaffSpellSO other)
    {
        return other != null && SpellName == other.SpellName;
    }
    
}
