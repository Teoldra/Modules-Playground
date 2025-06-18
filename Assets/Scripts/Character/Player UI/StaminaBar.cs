using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.value = stats.CurrentStamina / stats.MaxStamina;
    }
    private void OnEnable()
    {
        stats.OnStaminaChanged += UpdateBar;
    }

    private void OnDisable()
    {
        stats.OnStaminaChanged -= UpdateBar;
    }
    private void UpdateBar(float value)
    {
            slider.value = value / stats.MaxStamina;
        if (slider.value <= 0.05f)
        {
            slider.value = 0.05f; // Prevents the bar from going to zero
        }
      
    }
}
