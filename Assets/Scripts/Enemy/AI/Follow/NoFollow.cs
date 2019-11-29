using UnityEngine;

namespace Enemy.AI.Follow
{
    [CreateAssetMenu(fileName = "No Follow", menuName = "Enemy/Follow/No Follow")]
    public class NoFollow : FollowCommand
    {
        public override void Following(Transform self, Transform enemy, float speed)
        {
            self.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        public override void StopFollowing(Transform self)
        {
            self.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
