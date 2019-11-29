
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.AI.Escape
{
    [CreateAssetMenu(fileName = "Escape Change Position", menuName = "Enemy/Scape/Escape Change Position")]
    public class EscapeChangePosition : EscapeCommand
    {
        [SerializeField] private float timeToChangeDirection;
        private float currentTimeToChangeDirection;
        private Vector2 currentDirection;
        private bool canChangeDirection;
        private Rigidbody2D rigidbody2D;
        private float attackDistance;
        public override void Escaping(Transform self,Vector3 target,float speed)
        {
            if (!rigidbody2D)
            {
                rigidbody2D = self.GetComponent<Rigidbody2D>();
                attackDistance = self.GetComponent<EnemyController>().AttackDistance;
                currentDirection = RandomDirection();
            }

            
            if (currentTimeToChangeDirection <= 0 && canChangeDirection)
            {
                currentDirection = RandomDirection();
                currentTimeToChangeDirection = timeToChangeDirection;
                canChangeDirection = false;
                
            }
            if(Vector2.Distance(self.position,target) > attackDistance && !canChangeDirection)
            {

                rigidbody2D.velocity = speed * (target - self.position).normalized * Time.deltaTime;
                if (Vector2.Distance(self.position, target) <= attackDistance && currentTimeToChangeDirection <= 0)
                    canChangeDirection = true;
            }
            else if(Vector2.Distance(self.position, target) <= attackDistance && !canChangeDirection)
            {
                rigidbody2D.velocity = speed * currentDirection * Time.deltaTime;
                currentTimeToChangeDirection -= Time.deltaTime;
                if (Vector2.Distance(self.position, target) <= attackDistance && currentTimeToChangeDirection <= 0)
                    canChangeDirection = true;
            }
        }
        public Vector2 RandomDirection()
        {
            List<Vector2> directions = new List<Vector2>();
            directions.Add(Vector2.up);
            directions.Add(Vector2.right);
            directions.Add(Vector2.down);
            directions.Add(Vector2.left);
            
            return directions[Random.Range(0, directions.Count)];
        }

    }
}
