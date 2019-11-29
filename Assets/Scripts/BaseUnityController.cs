using UnityEngine;

public abstract class BaseUnityController : MonoBehaviour
{
    [SerializeField] protected BaseStats currentBaseStats;
    [SerializeField] private WeaponBase weapon;
    [SerializeField] protected string enemyTag;
    private float currentTimeToHealthRegen;

    public WeaponBase Weapon { get => weapon; set => weapon = value; }

    public abstract void OnTakeDamage(float attack);

    protected virtual void HealthRegen()
    {
        currentTimeToHealthRegen -= Time.deltaTime;
        if(currentTimeToHealthRegen <= 0 && currentBaseStats.CurrentHealth + currentBaseStats.HealthRegen <= currentBaseStats.Health)
        {
            currentBaseStats.CurrentHealth += currentBaseStats.HealthRegen;
            currentTimeToHealthRegen = currentBaseStats.TimeToHealthRegen;
        }
        if (currentTimeToHealthRegen <= 0 && currentBaseStats.CurrentHealth + currentBaseStats.HealthRegen > currentBaseStats.Health)
        {
            currentBaseStats.CurrentHealth = currentBaseStats.Health;
            currentTimeToHealthRegen = currentBaseStats.TimeToHealthRegen;
        }
    }

}
