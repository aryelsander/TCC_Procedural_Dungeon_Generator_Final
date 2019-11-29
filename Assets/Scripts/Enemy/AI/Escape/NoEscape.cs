using UnityEngine;

namespace Enemy.AI.Escape
{
    [CreateAssetMenu(fileName = "No Escape", menuName = "Enemy/Scape/No Scape")]
    public class NoEscape : EscapeCommand
    {
        public override void Escaping(Transform self,Vector3 target,float speed)
        {
            self.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
