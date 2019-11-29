using UnityEngine;
public abstract class WeaponBase : Equipment
{
    [Header("Weapon Configuration")]
    [SerializeField] private Projectile projectile;
    [SerializeField] private float timeToAttack;
    [SerializeField] private float shootForce;
    [SerializeField] private float timeToDestroy;

    public float TimeToDestroy { get => timeToDestroy; set => timeToDestroy = value; }
    public float ShootForce { get => shootForce; set => shootForce = value; }
    public float TimeToAttack { get => timeToAttack; set => timeToAttack = value; }
    public Projectile Projectile { get => projectile; set => projectile = value; }
    public override void SetEquipmentStats(Equipment current, Equipment getEquipment)
    {
        base.SetEquipmentStats(current, getEquipment);
    }
    public abstract void Attack(Transform spawnPosition, Vector3 targetPosition, string enemyTag,float attack);

}