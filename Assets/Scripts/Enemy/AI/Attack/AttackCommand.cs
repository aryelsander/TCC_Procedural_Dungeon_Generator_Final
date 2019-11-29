using UnityEngine;

namespace Enemy.AI.Attack
{
    public abstract class AttackCommand : ScriptableObject
    {

        public abstract void Attacking(BaseUnityController baseUnityController,Transform spawnPosition, Vector3 targetPosition, string enemyTag, float attack);
    }
}