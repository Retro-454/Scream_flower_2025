using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public player playerScript; // drag your Player object here
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = playerScript.maxHealth;
        slider.value = playerScript.health;
    }

    void Update()
    {
        slider.value = playerScript.health;
    }
}
