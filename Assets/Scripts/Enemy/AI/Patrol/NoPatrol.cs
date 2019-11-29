using UnityEngine;

namespace Enemy.AI.Patrol
{
    [CreateAssetMenu(fileName ="No Patrol",menuName ="Enemy/Patrol/No Patrol")]
    public class NoPatrol : PatrolCommand
    {
       
        public override void Patrolling(Transform self, Vector3 targetPosition,float speed)
        {
            self.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
