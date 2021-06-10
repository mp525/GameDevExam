using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    private Image hungerBar;
    [SerializeField] public float currentHunger;
    private float maxHunger = 100f;
    PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        hungerBar = GetComponent<Image>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHunger = playerManager.hunger;
        hungerBar.fillAmount = currentHunger / maxHunger;
    }

   

}
