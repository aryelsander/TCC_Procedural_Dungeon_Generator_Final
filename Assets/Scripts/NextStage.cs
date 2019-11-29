using Procedural.Map;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    private Room room;

    public Room Room { get => room; set => room = value; }

    public void CreateNextStage()
    {
        if(room.enemies == 0)
        {
            DungeonGenerator.Instance.CreateStage();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player)
            CreateNextStage();
    }

}
