using UnityEngine;

using Helper.Utilities;
[CreateAssetMenu(fileName ="One Shoot Weapon",menuName ="Equipments/Weapons/One Shoot Weapon")]
public class OneShootWeapon : WeaponBase
{

    public override void Attack(Transform spawnPosition,Vector3 targetPosition, string enemyTag, float attack)
    {
        Projectile projectileInstance = Instantiate(Projectile,spawnPosition.position,Quaternion.identity);
        projectileInstance.EnemyTag = enemyTag;
        projectileInstance.Attack = attack;
        HelperUtilities.LookAt2D(projectileInstance.transform, targetPosition,-90);
        projectileInstance.Rigidbody2D.AddForce(projectileInstance.transform.up * ShootForce);

        HelperUtilities.LookAt2D(projectileInstance.transform, targetPosition, 0);
        projectileInstance.DestroyOnCollision = true;
        Destroy(projectileInstance.gameObject, TimeToDestroy);

    }
}
