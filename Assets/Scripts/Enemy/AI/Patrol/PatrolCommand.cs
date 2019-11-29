using UnityEngine;

namespace Enemy.AI.Patrol
{
    public abstract class PatrolCommand : ScriptableObject
    {
        public abstract void Patrolling(Transform self, Vector3 targetPosition,float speed);

    }
}