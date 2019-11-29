using UnityEngine;

namespace Enemy.AI.Escape
{

   public abstract class EscapeCommand : ScriptableObject
    {

        public abstract void Escaping(Transform self,Vector3 target,float speed);
    }
}
