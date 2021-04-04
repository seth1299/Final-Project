using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Image HealthBar;
    private Image ManaBar;
    public float CurrentHealth;
    public float CurrentMana;
    private float MaxHealth = 100f;
    private float MaxMana = 100f;
    PlayerController Player;
    private void Start()
    {
        HealthBar = GetComponent<Image>();
        ManaBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerController>();
        HealthSystem healthSystem = new HealthSystem(100);
        Debug.Log("Health: "+healthSystem.GetHealth());
    }
    private void Update()
    {
        CurrentHealth = Player.Health;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
        CurrentMana = Player.Mana;
        ManaBar.fillAmount = CurrentMana / MaxMana;
    }
}
