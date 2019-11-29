using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{

    [System.Serializable]
    public class EnemyManager : MonoBehaviour
    {

        public EnemyList[] enemies;

        private static EnemyManager instance;

        public static EnemyManager Instance { get => instance; }

        public void Awake()
        {
            instance = this;
        }
        public EnemyRoomConfiguration[] GetEnemyArrayByPower(int power, EnemyRoomConfiguration[] enemyRoomConfiguration)
        {
            List<EnemyRoomConfiguration> enemies = new List<EnemyRoomConfiguration>();
            for (int i = 0; i < enemyRoomConfiguration.Length; i++)
            {
                if (enemyRoomConfiguration[i].enemyPower <= power)
                {
                    enemies.Add(enemyRoomConfiguration[i]);
                }
            }
            return enemies.ToArray();
        }

    }
}