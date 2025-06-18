using System;
using UnityEngine;
using UnityEngine.Events;



public class PlayerStats : MonoBehaviour
{   
    [Header("Health")]
    public float MaxHealth = 100f;
    public float CurrentHealth { get; private set; }
    public event Action<float> OnHealthChanged;

    [Header("Stamina")]
    public float MaxStamina = 100f;
    public float CurrentStamina;
    public event Action<float> OnStaminaChanged;

    [Header("Stamina Settings")]
    public float StaminaRegenRate = 10f;
    public float StaminaRegenDelay = 1f;
    public float StaminaUnlockThreshold = 20f;
    private float lastStaminaUseTime;

    public bool IsStaminaLocked { get; private set; } = false;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        CurrentStamina = MaxStamina;
    }

    private void Update()
    {
        HandleStaminaRegen();
    }

    private void HandleStaminaRegen()
    {
        if (Time.time > lastStaminaUseTime + StaminaRegenDelay && CurrentStamina < MaxStamina)
        {
            RegenerateStamina(StaminaRegenRate * Time.deltaTime);
        }

        if (IsStaminaLocked && CurrentStamina >= StaminaUnlockThreshold)
        {
            IsStaminaLocked = false;
        }
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0f);
        OnHealthChanged?.Invoke(CurrentHealth);
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
        OnHealthChanged?.Invoke(CurrentHealth);
    }

    public void UseStamina(float amount)
    {
        if (CanUseStamina(amount))
        {
            CurrentStamina -= amount;
            lastStaminaUseTime = Time.time;
            OnStaminaChanged?.Invoke(CurrentStamina);

            if (CurrentStamina <= 5f)
            {
                IsStaminaLocked = true;
            }
        }
    }

    public bool CanUseStamina(float amount = 1f)
    {
        return CurrentStamina >= amount && !IsStaminaLocked;
    }

    public void RegenerateStamina(float amount)
    {
        CurrentStamina = Mathf.Min(CurrentStamina + amount, MaxStamina);
        OnStaminaChanged?.Invoke(CurrentStamina);
    }
}
