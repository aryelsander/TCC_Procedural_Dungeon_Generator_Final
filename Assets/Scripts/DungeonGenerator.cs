using Helper.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Procedural.Map
{
    public class DungeonGenerator : MonoBehaviour
    {
        public int dungeonSize;
        public GameObject grid;
        public NextStage nextStage;
        public int maxEnemyPowerRoom;
        public Vector2 roomDistance = Vector2.zero;
        public PlayerController playerControllerReference;
        private List<Vector2> roomPosition = new List<Vector2>();
        
        private TileBaseData chooseTileTheme;
        public static List<GameObject> rooms = new List<GameObject>();
        private static DungeonGenerator instance;

        public static Action OnClearDungeon;
        // T R L B
        public GameObject TRLB;
        //T R B
        public GameObject TRB;
        //T L B
        public GameObject TLB;
        //R B L
        public GameObject RBL;
        //R L T
        public GameObject RLT;
        //R L 
        public GameObject RL;
        //R B 
        public GameObject RB;
        //R T 
        public GameObject RT;
        //B L
        public GameObject BL;
        //B T
        public GameObject BT;
        //T L
        public GameObject TL;
        //T
        public GameObject T;
        //L
        public GameObject L;
        //R
        public GameObject R;
        //B
        public GameObject B;

        public static DungeonGenerator Instance { get => instance;}

        private void Awake()
        {
            instance = this;
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            playerControllerReference = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
           
            CreateStage();
        }
        private void Update()
        {
          //  if (Input.GetKeyDown(KeyCode.Space))
         //   {
          //      CreateStage();
         //   }
        }
        public void GenerateDungeonMesh()
        {
            if (dungeonSize == 0)
                return;
            
            Vector2 currentPosition = Vector2.zero;
            for (int i = 0; i < dungeonSize; i++)
            {

                Vector2 randomPosition = RandomPosition(currentPosition);
                roomPosition.Add(currentPosition);
                currentPosition = randomPosition;
            }
           
        }

        public Vector2 RandomPosition(Vector2 currentPosition)
        {


            List<Vector2> positions = new List<Vector2>();

            do
            {
                if (!ExistRoom(GetSide(Side.TOP, currentPosition))) positions.Add(GetSide(Side.TOP, currentPosition));
                if (!ExistRoom(GetSide(Side.BOTTOM, currentPosition))) positions.Add(GetSide(Side.BOTTOM, currentPosition));
                if (!ExistRoom(GetSide(Side.RIGHT, currentPosition))) positions.Add(GetSide(Side.RIGHT, currentPosition));
                if (!ExistRoom(GetSide(Side.LEFT, currentPosition))) positions.Add(GetSide(Side.LEFT, currentPosition));

                if(positions.Count == 0)
                {
                    List<Side> allSides = GetRoomSides(currentPosition);
                    int changePosition = UnityEngine.Random.Range(0, allSides.Count);
                    currentPosition = GetSide(allSides[changePosition],currentPosition);
                }

            } while (positions.Count == 0);


            return positions[UnityEngine.Random.Range(0, positions.Count)];

        }
        public bool ExistRoom(Vector2 currentPosition)
        {
            if (dungeonSize == 0)
                return false;

            for (int i = 0; i < roomPosition.Count; i++)
            {
                if (currentPosition == roomPosition[i])
                    return true;
            }

            return false;
        }
        public void CreateStage()
        {
            ClearDungeon();
            GenerateDungeonMesh();
            GameManager.instance.stage++;
            for (int i = 0; i < roomPosition.Count; i++)
            {
                CreateRoom(roomPosition[i]);
                if (i == roomPosition.Count - 1)
                {
                   NextStage nextStageInstace = Instantiate(nextStage, rooms[i].GetComponent<Room>().playerStartPosition.transform.position, Quaternion.identity);
                   nextStageInstace.Room = rooms[i].GetComponent<Room>();
                   nextStageInstace.Room.NextStage = nextStageInstace; 
                }
            }
            TilemapController.Instance.SetAllTileBaseData(rooms);
            rooms[0].GetComponent<Room>().minimapIcon.SetActive(true);
            rooms[0].GetComponent<Room>().ActiveRoom();
            playerControllerReference.transform.position = rooms[0].GetComponent<Room>().playerStartPosition.position;
            
            playerControllerReference.SetupCamera();
            dungeonSize++;
        }

        private void ClearDungeon()
        {
            if (roomPosition.Count > 0)
            {
                roomPosition.Clear();
            }
            if (rooms.Count > 0)
            {
                for (int i = 0; i < rooms.Count; i++)
                {
                    Destroy(rooms[i]);

                }
                rooms.Clear();
            }
            OnClearDungeon?.Invoke();
        }

        public void CreateRoom(Vector2 currentPosition)
        {
            GameObject roomInstance = null;
            if (GetRoomSides(currentPosition).Contains(Side.TOP) && GetRoomSides(currentPosition).Contains(Side.RIGHT) && GetRoomSides(currentPosition).Contains(Side.BOTTOM) && GetRoomSides(currentPosition).Contains(Side.LEFT))
            {
                roomInstance = Instantiate(TRLB, currentPosition, Quaternion.identity);           
                roomInstance.transform.SetParent(grid.transform, false);
                // T R L B
            }
            else if (GetRoomSides(currentPosition).Contains(Side.TOP) && GetRoomSides(currentPosition).Contains(Side.RIGHT) && GetRoomSides(currentPosition).Contains(Side.BOTTOM))
            {
                roomInstance = Instantiate(TRB, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);

                //T R B
            }
            else if (GetRoomSides(currentPosition).Contains(Side.TOP) && GetRoomSides(currentPosition).Contains(Side.LEFT) && GetRoomSides(currentPosition).Contains(Side.BOTTOM))
            {
                roomInstance = Instantiate(TLB, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);

                //T L B
            }
            else if (GetRoomSides(currentPosition).Contains(Side.RIGHT) && GetRoomSides(currentPosition).Contains(Side.LEFT) && GetRoomSides(currentPosition).Contains(Side.BOTTOM))
            {
                roomInstance = Instantiate(RBL, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);

                //R B L
            }
            else if (GetRoomSides(currentPosition).Contains(Side.RIGHT) && GetRoomSides(currentPosition).Contains(Side.LEFT) && GetRoomSides(currentPosition).Contains(Side.TOP))
            {
                roomInstance = Instantiate(RLT, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);

                //R L T
            }
            else if (GetRoomSides(currentPosition).Contains(Side.RIGHT) && GetRoomSides(currentPosition).Contains(Side.LEFT))
            {
                roomInstance = Instantiate(RL, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);


                //R L 
            }
            else if (GetRoomSides(currentPosition).Contains(Side.RIGHT) && GetRoomSides(currentPosition).Contains(Side.BOTTOM))
            {
                roomInstance = Instantiate(RB, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);


                //R B 
            }
            else if (GetRoomSides(currentPosition).Contains(Side.RIGHT) && GetRoomSides(currentPosition).Contains(Side.TOP))
            {
                roomInstance = Instantiate(RT, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);


                //R T 
            }
            else if (GetRoomSides(currentPosition).Contains(Side.BOTTOM) && GetRoomSides(currentPosition).Contains(Side.LEFT))
            {
                roomInstance = Instantiate(BL, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);


                //B L
            }
            else if (GetRoomSides(currentPosition).Contains(Side.BOTTOM) && GetRoomSides(currentPosition).Contains(Side.TOP))
            {
                roomInstance = Instantiate(BT, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);


                //B T
            }
            else if (GetRoomSides(currentPosition).Contains(Side.TOP) && GetRoomSides(currentPosition).Contains(Side.LEFT))
            {
                roomInstance = Instantiate(TL, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);


                //T L
            }
            else if (GetRoomSides(currentPosition).Contains(Side.TOP))
            {
                roomInstance = Instantiate(T, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);


                //T
            }
            else if (GetRoomSides(currentPosition).Contains(Side.LEFT))
            {
                roomInstance = Instantiate(L, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);


                //L
            }
            else if (GetRoomSides(currentPosition).Contains(Side.RIGHT))
            {
                roomInstance = Instantiate(R, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);


                //R
            }
            else if (GetRoomSides(currentPosition).Contains(Side.BOTTOM))
            {
                roomInstance = Instantiate(B, currentPosition, Quaternion.identity);
                roomInstance.transform.SetParent(grid.transform, false);


                //B
            }

            rooms.Add(roomInstance);
            

        }
        public List<Side> GetRoomSides(Vector2 currentPosition)
        {

            List<Side> sides = new List<Side>();
            if (ExistRoom(GetSide(Side.TOP, currentPosition))) sides.Add(Side.TOP);
            if (ExistRoom(GetSide(Side.RIGHT, currentPosition))) sides.Add(Side.RIGHT);
            if (ExistRoom(GetSide(Side.BOTTOM, currentPosition))) sides.Add(Side.BOTTOM);
            if (ExistRoom(GetSide(Side.LEFT, currentPosition))) sides.Add(Side.LEFT);

            return sides;
        }
        public Vector2 GetSide(Side side, Vector2 currentPosition)
        {
            if (side == Side.TOP) return currentPosition + (Vector2.up * roomDistance.y);
            else if (side == Side.BOTTOM) return currentPosition + (Vector2.down * roomDistance.y);
            else if (side == Side.RIGHT) return currentPosition + (Vector2.right * roomDistance.x);
            else return currentPosition + (Vector2.left * roomDistance.x);

        }
    }

}
