using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    [SerializeField] private float currentHealth;
    private float maxHealth = 100f;
    PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerManager.health;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
