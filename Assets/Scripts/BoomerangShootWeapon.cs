using Helper.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "Boomerang Shoot Weapon", menuName = "Equipments/Weapons/Boomerang Shoot Weapon")]
public class BoomerangShootWeapon : WeaponBase
{
    [SerializeField] private Vector2 frequency;
    [SerializeField] private Vector2 amplitude;

    public Vector2 Frequency { get => frequency; set => frequency = value; }
    public Vector2 Amplitude { get => amplitude; set => amplitude = value; }

    public override void SetEquipmentStats(Equipment current, Equipment getEquipment)
    {
        base.SetEquipmentStats(current, getEquipment);

    }


    public override void Attack(Transform spawnPosition, Vector3 targetPosition, string enemyTag, float attack)
    {
        Projectile projectileInstance = Instantiate(Projectile, spawnPosition.position, Quaternion.identity);
        projectileInstance.EnemyTag = enemyTag;
        projectileInstance.Attack = attack;
        HelperUtilities.LookAt2D(projectileInstance.transform, targetPosition, -90);
        projectileInstance.DestroyOnCollision = true;
        Destroy(projectileInstance.gameObject, TimeToDestroy);
    }
}
