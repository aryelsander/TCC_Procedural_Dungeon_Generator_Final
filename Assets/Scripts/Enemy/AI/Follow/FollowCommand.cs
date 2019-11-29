using UnityEngine;
namespace Enemy.AI.Follow
{
    public abstract class FollowCommand : ScriptableObject
    {

        public abstract void Following(Transform self,Transform enemy,float speed);
        public abstract void StopFollowing(Transform self);
    }
}