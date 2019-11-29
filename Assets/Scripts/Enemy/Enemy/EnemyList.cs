namespace Enemy
{
    [System.Serializable]
    public class EnemyRoomConfiguration
    {
        public int enemyPower;
        public EnemyController enemy;
    }
    [System.Serializable]
    public class EnemyList
    {
        public EnemyRoomConfiguration[] enemyByArea;

    }
}