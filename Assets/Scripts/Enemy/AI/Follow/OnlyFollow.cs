using UnityEngine;

namespace Enemy.AI.Follow
{
    [CreateAssetMenu(fileName ="Only Follow",menuName ="Enemy/Follow/Only Follow")]
    public class OnlyFollow : FollowCommand
    {
        public override void Following(Transform self,Transform enemy,float speed)
        {

                self.GetComponent<Rigidbody2D>().velocity = (enemy.position - self.position).normalized * speed * Time.deltaTime;
        
        }
        public override void StopFollowing(Transform self)
        {
            self.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
