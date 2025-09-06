using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    [SerializeField]TMP_Text m_spellNameText;
    [SerializeField]TMP_Text m_spellUsesText;
    [SerializeField] Image m_spellIcon;
    public void UpdateSpellUI (Staff.SpellData  spellData)
    {
        if(m_spellNameText is null || m_spellUsesText is null || m_spellIcon is null) return;
        m_spellNameText.text = spellData.SelectedSpell;
        string spellUses = spellData.SelectedSpellUses is null ? "∞" : spellData.SelectedSpellUses.ToString();
        m_spellUsesText.text = "Uses: " + spellUses;
        if(spellData.SpellSprite is null) return;
        m_spellIcon.sprite = spellData.SpellSprite;
    }
}
