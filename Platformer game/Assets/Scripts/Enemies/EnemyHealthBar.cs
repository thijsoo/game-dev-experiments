using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    private Damageble enemyDamageble;

    private void Awake()
    {
        GameObject enemy = gameObject.transform.parent.parent.gameObject;
        if (enemy == null)
        {
            Debug.LogError("Player not found!");
        }
        enemyDamageble = enemy.GetComponent<Damageble>();
    }

    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(enemyDamageble.CurrentHealth,enemyDamageble.MaxHealth);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnEnable()
    {
        enemyDamageble.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        enemyDamageble.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth,maxHealth);
    }
}
