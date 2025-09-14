using System;
using AudioSystem;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public abstract class StaffSpell
{
    public string SpellName;
    public SoundData CastSound;
    public Sprite SpellSprite;
    [HideInInspector] public Staff SpellOwner;

    public virtual void Initialize(Staff owner)
    {
        SpellOwner = owner;
    }
    public virtual Color SpellBallColor { get; private set; }
    public abstract uint? UseCount { get; }
    public abstract void ExecuteAttack(Vector3 position, Vector3 direction, Quaternion rotation);
}
