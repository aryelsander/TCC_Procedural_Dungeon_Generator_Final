using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TilemapController : MonoBehaviour
{
    public TileBaseData activedTile;
    public TileBaseData[] stageTile;
    private TileBaseData chooseTileTheme;
    private static TilemapController instance;
    private int chooseTileIndex;
    public static TilemapController Instance { get => instance;}
    public int ChooseTileIndex { get => chooseTileIndex;}

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        

    }
    public void SetAllTileBaseData(List<GameObject> rooms)
    {
        chooseTileIndex = Random.Range(0, stageTile.Length);
        chooseTileTheme = stageTile[chooseTileIndex];

        if (chooseTileIndex == 0)
            return;
        foreach (GameObject room in rooms)
        {
            Tilemap groundRoom = room.GetComponent<Room>().groundRoom;
            Tilemap wallRoom = room.GetComponent<Room>().wallRoom;
            Tilemap wallBlockRoom = room.GetComponent<Room>().wallBlockRoom;
            SetTileBaseData(groundRoom, wallRoom,wallBlockRoom, activedTile, chooseTileTheme);
        }
    }
    public void SetTileBaseData(Tilemap groundTileMap,Tilemap wallTileMap,Tilemap wallBlockTileMap, TileBaseData currentBaseData, TileBaseData changedBaseData)
    {
        wallTileMap.SwapTile(currentBaseData.wallTile, changedBaseData.wallTile);
        wallBlockTileMap.SwapTile(currentBaseData.wallBlockTile, changedBaseData.wallBlockTile);
        groundTileMap.SwapTile(currentBaseData.groundTile, changedBaseData.groundTile);
        groundTileMap.SwapTile(currentBaseData.topLeftTile, changedBaseData.topLeftTile);
        groundTileMap.SwapTile(currentBaseData.TopMidlleTile, changedBaseData.TopMidlleTile);
        groundTileMap.SwapTile(currentBaseData.TopRightTile, changedBaseData.TopRightTile);
        groundTileMap.SwapTile(currentBaseData.RightTile, changedBaseData.RightTile);
        groundTileMap.SwapTile(currentBaseData.LeftTile, changedBaseData.LeftTile);
        groundTileMap.SwapTile(currentBaseData.bottomLeftTile, changedBaseData.bottomLeftTile);
        groundTileMap.SwapTile(currentBaseData.bottomMidlleTile, changedBaseData.bottomMidlleTile);
        groundTileMap.SwapTile(currentBaseData.bottomRightTile, changedBaseData.bottomRightTile);
    }
}
