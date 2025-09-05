using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    uint m_currentHealth;
    uint m_maxHealth;
    [SerializeField] Image m_healthBar;
    public void InitializeHealthBar(uint currentHealth, uint maxHealth)
    {
        m_currentHealth = currentHealth;
        m_maxHealth = maxHealth;
        
        m_healthBar.fillAmount = m_currentHealth / (float)m_maxHealth;
    }
    public void UpdateHealthBar(uint oldHealth, uint newHealth)
    {
        m_currentHealth = newHealth;
        m_healthBar.fillAmount = newHealth / (float)m_maxHealth;
    }
}
