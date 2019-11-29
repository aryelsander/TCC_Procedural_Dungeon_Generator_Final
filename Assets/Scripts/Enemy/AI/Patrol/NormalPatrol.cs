
using UnityEngine;

namespace Enemy.AI.Patrol
{
    [CreateAssetMenu(fileName = "Normal Patrol", menuName = "Enemy/Patrol/Normal Patrol")]
    public class NormalPatrol : PatrolCommand
    {
        public override void Patrolling(Transform self, Vector3 targetPosition,float speed)
        {
            
            self.GetComponent<Rigidbody2D>().velocity = (targetPosition - self.position).normalized * speed * Time.deltaTime;
        }
    }
}
