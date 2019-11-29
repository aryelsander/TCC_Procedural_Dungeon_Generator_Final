using UnityEngine;
using UnityEngine.Tilemaps;
using Cinemachine;
using Procedural.Map;
using Enemy;

public class Room : MonoBehaviour
{
    public Tilemap groundRoom;
    public Tilemap wallRoom;
    public Tilemap wallBlockRoom;
    public Transform playerStartPosition;
    public Transform[] enemySpawns;
    public Transform wallBlock;
    public CinemachineVirtualCamera virtualCameraRoom;
    public GameObject minimapIcon;
    public int enemies;
    public bool active = false;
    public bool currentRoom;
    private NextStage nextStage;
    private Vector2 randomSpawnPosition = new Vector3(1.5f,1.5f,0);
    public NextStage NextStage { get => nextStage; set => nextStage = value; }

    public void ActiveRoom()
    {
        active = true;
        int enemyPowerRoom = DungeonGenerator.Instance.maxEnemyPowerRoom;
        EnemyRoomConfiguration[] enemyList = EnemyManager.Instance.enemies[TilemapController.Instance.ChooseTileIndex].enemyByArea;
        wallBlock.gameObject.SetActive(true);

        EnemyRoomConfiguration[] enemyListByPower;
        int index;
        do
        {
            enemyListByPower = EnemyManager.Instance.GetEnemyArrayByPower(enemyPowerRoom, enemyList);
            index = Random.Range(0, enemyListByPower.Length);
            EnemyController enemy = Instantiate(enemyListByPower[index].enemy,new Vector3(Random.Range(-randomSpawnPosition.x, randomSpawnPosition.x),Random.Range(-randomSpawnPosition.y, randomSpawnPosition.y),0) + enemySpawns[Random.Range(0, enemySpawns.Length)].position, Quaternion.identity);
            enemy.EnemyScore = enemyListByPower[index].enemyPower * GameManager.instance.stage;
            enemy.EnemyRoom = this;
            enemy.ApplyStatsByPower();
            enemyPowerRoom -= enemyListByPower[index].enemyPower;
            enemies++;
        }
        while (EnemyManager.Instance.GetEnemyArrayByPower(enemyPowerRoom, enemyList).Length > 0 || enemyPowerRoom > 0);
    }
    public void EnemyDeath()
    {
        enemies--;
        if(enemies == 0)
        {    
            
            wallBlock.gameObject.SetActive(false);
        }
    }
}
