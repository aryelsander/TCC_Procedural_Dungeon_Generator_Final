using UnityEngine;

namespace Enemy.AI.Attack
{
    [CreateAssetMenu(fileName = "Attack Target", menuName = "Enemy/Attack/Attack Target")]
    public class AttackTarget : AttackCommand
    {
        public override void Attacking(BaseUnityController baseUnityController,Transform spawnPosition, Vector3 targetPosition, string enemyTag, float attack)
        {

            baseUnityController.GetComponent<BaseUnityController>().Weapon.Attack(spawnPosition, targetPosition, enemyTag, attack);

        }
    }
}