using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;
    [SerializeField] Image healthImage;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Text enemiesKilled;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        enemiesKilled.text = "Kills: " + GetComponent<PlayerController>().GetKills().ToString();
    }

    public void AddHealth(float extra)
    {
        currentHealth += extra;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        // Update health UI here
        if (currentHealth <= 0)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void UpdateHealthBar()
    {
        healthImage.fillAmount = currentHealth/maxHealth;
    }
}
