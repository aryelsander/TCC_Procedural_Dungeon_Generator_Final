using UnityEngine;
using TMPro;
public class EquipmentHud : MonoBehaviour
{

    public GameObject EquipmentHUD;
    public Color neutralColor;
    public Color positiveColor;
    public Color negativeColor;
    [Header("Current Equipment")]
    public TextMeshProUGUI currentName;
    public TextMeshProUGUI currentHealth;
    public TextMeshProUGUI currentAttack;
    public TextMeshProUGUI currentDefense;
    public TextMeshProUGUI currentMovespeed;
    public TextMeshProUGUI currentHealthRegen;

    [Header("New Equipment")]
    public TextMeshProUGUI newName;
    public TextMeshProUGUI newHealth;
    public TextMeshProUGUI newAttack;
    public TextMeshProUGUI newDefense;
    public TextMeshProUGUI newMovespeed;
    public TextMeshProUGUI newHealthRegen;

    private static EquipmentHud instance;

    public static EquipmentHud Instance { get => instance;}

    private void Awake()
    {
        instance = this;
    }

    public void ShowEquipmentHud(Equipment currentEquipment,Equipment newEquipment)
    {
        EquipmentHUD.SetActive(true);
        currentName.text = currentEquipment.name;
        currentHealth.text = $"Health: {currentEquipment.Health.ToString()}";
        currentAttack.text = $"Attack: {currentEquipment.Attack.ToString()}";
        currentDefense.text = $"Defense: {currentEquipment.Defense.ToString()}";
        currentMovespeed.text = $"Movespeed: {currentEquipment.Movespeed.ToString()}";
        currentHealthRegen.text = $"HealthRegen: {currentEquipment.HealthRegen.ToString()}";


        newName.text = newEquipment.name;
        newHealth.text = $"Health: {newEquipment.Health.ToString()}";
        newAttack.text = $"Attack: {newEquipment.Attack.ToString()}";
        newDefense.text = $"Defense: {newEquipment.Defense.ToString()}";
        newMovespeed.text = $"Movespeed: {newEquipment.Movespeed.ToString()}";
        newHealthRegen.text = $"Health Regen: {newEquipment.HealthRegen.ToString()}";

        SetTextColor(currentEquipment, newEquipment);

    }

    private void SetTextColor(Equipment currentEquipment, Equipment newEquipment)
    {
        if (newEquipment.Health > currentEquipment.Health)
            newHealth.color = positiveColor;
        else if (newEquipment.Health < currentEquipment.Health)
            newHealth.color = negativeColor;
        else
            newHealth.color = neutralColor;

        if (newEquipment.Attack > currentEquipment.Attack)
            newAttack.color = positiveColor;
        else if (newEquipment.Attack < currentEquipment.Attack)
            newAttack.color = negativeColor;
        else
            newAttack.color = neutralColor;

        if (newEquipment.Defense > currentEquipment.Defense)
            newDefense.color = positiveColor;
        else if (newEquipment.Defense < currentEquipment.Defense)
            newDefense.color = negativeColor;
        else
            newDefense.color = neutralColor;


        if (newEquipment.Movespeed > currentEquipment.Movespeed)
            newMovespeed.color = positiveColor;
        else if (newEquipment.Movespeed < currentEquipment.Movespeed)
            newMovespeed.color = negativeColor;
        else
            newMovespeed.color = neutralColor;


        if (newEquipment.HealthRegen > currentEquipment.HealthRegen)
            newHealthRegen.color = positiveColor;
        else if (newEquipment.HealthRegen < currentEquipment.HealthRegen)
            newHealthRegen.color = negativeColor;
        else
            newHealthRegen.color = neutralColor;
    }

    public void HideEquipmentHud()
    {
        EquipmentHUD.SetActive(false);
    }
}
