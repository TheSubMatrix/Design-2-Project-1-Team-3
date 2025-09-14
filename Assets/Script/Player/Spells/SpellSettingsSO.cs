using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Settings", menuName = "Scriptable Objects/Spell Settings")]
public class SpellSettingsSO : ScriptableObject
{
    [SerializeReference, SubclassList(typeof(StaffSpell))] public StaffSpell SpellInstance;
}
