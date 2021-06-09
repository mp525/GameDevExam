using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarAI : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth=100;
    //private float hp1;
    private float maxHealth = 100;

    // Start is called before the first frame update
    public void Start()
    {
        healthBar = GetComponent<Image>();
       
    }

    // Update is called once per frame
    public void Update()
    {
        healthBar.fillAmount = currentHealth/100;
    }

    public void TakeDamage(float damage)
    {
        currentHealth =- damage;
    }
    public void SetCurrentHealth(float hp){
        currentHealth=hp;
    }
}
