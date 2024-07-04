using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;
    private Damageble playerDamageble;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found!");
        }
        playerDamageble = player.GetComponent<Damageble>();
    }

    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageble.CurrentHealth,playerDamageble.MaxHealth);
        healthBarText.text = "Health " + playerDamageble.CurrentHealth + "/" + playerDamageble.MaxHealth;
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnEnable()
    {
        playerDamageble.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageble.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth,maxHealth);
        healthBarText.text = "Health " + newHealth + "/" +maxHealth;
    }
}
