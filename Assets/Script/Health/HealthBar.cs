using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _HealthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateHealthBar(float maxhealthBar, float currentHealthBar)
    {
        _HealthBar.fillAmount = currentHealthBar / maxhealthBar;
    }
}
