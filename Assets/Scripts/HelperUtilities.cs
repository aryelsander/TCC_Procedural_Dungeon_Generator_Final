using System;
using UnityEngine;

namespace Helper.Utilities
{
    public enum Side
    {
        TOP = 1,
        RIGHT = 2,
        BOTTOM = 3,
        LEFT = 4
    }
    public static class HelperUtilities
    {
        public static void LookAt2D(Transform self,Vector3 target,float angle)
        {

           Vector3 direction = (target - self.position).normalized;
           float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           self.rotation = Quaternion.Euler(0, 0, rotationZ + angle);

        }

    

    }
}
