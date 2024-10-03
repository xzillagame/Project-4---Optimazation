using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] private PlayerHealthSO playerHealthSO;

    private void OnEnable()
    {
        playerHealthSO.OnPlayerTakeDamage += OnPlayerDamaged;
    }

    private void OnDisable()
    {
        playerHealthSO.OnPlayerTakeDamage -= OnPlayerDamaged;
    }

    private void OnPlayerDamaged(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }



}
