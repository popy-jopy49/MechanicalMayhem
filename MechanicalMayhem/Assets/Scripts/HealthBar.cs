using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Slider slider;

    // Reference slider
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Set max health on slider
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // set current health
    public void SetHealth(float health)
    {
        slider.value = health;
    }

}
