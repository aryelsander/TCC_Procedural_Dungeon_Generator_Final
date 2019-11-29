using Helper.Utilities;
using UnityEngine;
using Math = System.Math;
[CreateAssetMenu(fileName = "Piercing Shoot Weapon", menuName = "Equipments/Weapons/Piercing Shoot Weapon")]
public class PiercingShootWeapon : WeaponBase
{
    public override void Attack(Transform spawnPosition, Vector3 targetPosition, string enemyTag, float attack)
    {
        Projectile projectileInstance = Instantiate(Projectile, spawnPosition.position, Quaternion.identity);
        projectileInstance.EnemyTag = enemyTag;
        projectileInstance.Attack = (float) Math.Round(attack / 0.8f,2);
        HelperUtilities.LookAt2D(projectileInstance.transform, targetPosition, -90);
        projectileInstance.Rigidbody2D.AddForce(projectileInstance.transform.up * ShootForce);

        HelperUtilities.LookAt2D(projectileInstance.transform, targetPosition, 0);
        projectileInstance.DestroyOnCollision = false;
        Destroy(projectileInstance.gameObject, TimeToDestroy);

    }
}
