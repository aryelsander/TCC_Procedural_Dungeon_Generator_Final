using Helper.Utilities;
using UnityEngine;
using Math = System.Math;
[CreateAssetMenu(fileName = "Three Shoot Weapon", menuName = "Equipments/Weapons/Three Shoot Weapon")]
public class ThreeShootWeapon : WeaponBase
{
    public override void Attack(Transform spawnPosition, Vector3 targetPosition, string enemyTag, float attack)
    {
        float rotation = -75;
        for (int i = 0; i < 3; i++)
        {
            Projectile projectileInstance = Instantiate(Projectile, spawnPosition.position, Quaternion.identity);
            projectileInstance.EnemyTag = enemyTag;
            projectileInstance.Attack = (float)Math.Round(attack/2f,2);
            HelperUtilities.LookAt2D(projectileInstance.transform, targetPosition, rotation);
            projectileInstance.Rigidbody2D.AddForce(projectileInstance.transform.up * ShootForce);

            HelperUtilities.LookAt2D(projectileInstance.transform, targetPosition, 0);
            projectileInstance.DestroyOnCollision = true;
            Destroy(projectileInstance.gameObject, TimeToDestroy);
            rotation -= 15;
        }

    }
}
