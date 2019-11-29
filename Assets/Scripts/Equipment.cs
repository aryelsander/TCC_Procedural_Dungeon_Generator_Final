using UnityEngine;
using System;
using Random = UnityEngine.Random;
public enum EquipmentSlot
{
    Helm,
    Body,
    Legs,
    Weapon,
}
[CreateAssetMenu(fileName = "Equipment", menuName = "Equipments/Equipment")]
public class Equipment : ScriptableObject
{
    [Header("Minimun Equipment Stats")]
    [SerializeField] private BaseStats minimumEquipmentStats;

    [Header("Maximum Equipment Stats")]
    [SerializeField] private BaseStats maximumEquipmentStats;
    [SerializeField] private Sprite equipmentSprite; 
    [SerializeField] private float health;
    [SerializeField] private float attack;
    [SerializeField] private float defense;
    [SerializeField] private float movespeed;
    [SerializeField] private float healthRegen;
    public EquipmentSlot slot;

    public virtual void SetEquipmentStats(Equipment current, Equipment getEquipment)
    {
        current.health = getEquipment.health;
        current.attack = getEquipment.attack;
        current.defense = getEquipment.defense;
        current.movespeed = getEquipment.movespeed;
        current.healthRegen = getEquipment.healthRegen;
        current.equipmentSprite = getEquipment.equipmentSprite;
    }
    public void SetRandomStats()
    {
        
        health = (float) Math.Round(GameManager.instance.stage * Random.Range(minimumEquipmentStats.Health,maximumEquipmentStats.Health),1);
        attack = (float)Math.Round(GameManager.instance.stage * Random.Range(minimumEquipmentStats.Attack, maximumEquipmentStats.Attack),1);
        defense = (float)Math.Round(GameManager.instance.stage * Random.Range(minimumEquipmentStats.Defense, maximumEquipmentStats.Defense),1);
        movespeed = (float)Math.Round(GameManager.instance.stage * Random.Range(minimumEquipmentStats.Movespeed, maximumEquipmentStats.Movespeed),2);
        healthRegen = (float)Math.Round(GameManager.instance.stage * Random.Range(minimumEquipmentStats.HealthRegen, maximumEquipmentStats.HealthRegen),1);
    }

    public float Health { get => health; set => health = value; }
    public float Attack { get => attack; set => attack = value; }
    public float Defense { get => defense; set => defense = value; }
    public float Movespeed { get => movespeed; set => movespeed = value; }
    public float HealthRegen { get => healthRegen; set => healthRegen = value; }
    public BaseStats MinimumEquipmentStats { get => minimumEquipmentStats; set => minimumEquipmentStats = value; }
    public BaseStats MaximumEquipmentStats { get => maximumEquipmentStats; set => maximumEquipmentStats = value; }
    public Sprite EquipmentSprite { get => equipmentSprite;}
}
