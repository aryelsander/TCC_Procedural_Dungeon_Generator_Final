using UnityEngine;
using Cinemachine;
using System.Collections;
using Procedural.Map;

public class RoomTrigger : MonoBehaviour
{
    public Room room;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerControllerReference = collision.GetComponent<PlayerController>();
        if (!playerControllerReference)
            return;
        if (!room.virtualCameraRoom.isActiveAndEnabled)
        {
            playerControllerReference.ChangeCamera(room.virtualCameraRoom);
            StartCoroutine(PlayerPause());
        }
    
        if (!room.minimapIcon.activeSelf)
        {
            room.minimapIcon.SetActive(true);
            if(room.NextStage)
                room.NextStage.gameObject.SetActive(true);
        }
        if (!room.active)
        {
            room.ActiveRoom();
        }
    }

    public IEnumerator PlayerPause()
    {
        GameManager.instance.GameState = GameState.PAUSED;
        yield return new WaitForSeconds(1);
        GameManager.instance.GameState = GameState.INGAME;
    }

}
