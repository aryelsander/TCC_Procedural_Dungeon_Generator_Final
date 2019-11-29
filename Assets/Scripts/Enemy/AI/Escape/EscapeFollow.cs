using UnityEngine;

namespace Enemy.AI.Escape
{

    [CreateAssetMenu(fileName = "Escape Follow", menuName = "Enemy/Scape/Escape Follow")]
    public class EscapeFollow : EscapeCommand
    {
        private Rigidbody2D rigidbody2D;
        public override void Escaping(Transform self,Vector3 target, float speed)
        {
            if (!rigidbody2D)
                rigidbody2D = self.GetComponent<Rigidbody2D>();

            rigidbody2D.velocity = (target - self.position).normalized * speed * Time.deltaTime;
        }
    }
}
