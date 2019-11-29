using UnityEngine;
[System.Serializable]
public class BaseStats
{
    [SerializeField] private float health;
    [SerializeField] private float attack;
    [SerializeField] private float defense;
    [SerializeField] private float movespeed;
    [SerializeField] private float healthRegen;
    [SerializeField] private float timeToHealthRegen;

    private float currentHealth;
    public float Health { get => health; set => health = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float Attack { get => attack; set => attack = value; }
    public float Movespeed { get => movespeed; set => movespeed = value; }
    public float HealthRegen { get => healthRegen; set => healthRegen = value; }
    public float TimeToHealthRegen { get => timeToHealthRegen; set => timeToHealthRegen = value; }
    public float Defense { get => defense; set => defense = value; }
} 