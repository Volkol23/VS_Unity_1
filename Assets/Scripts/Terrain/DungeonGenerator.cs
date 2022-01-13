using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool Visited = false;
        public bool[] Status = new bool[4];
    }

    [System.Serializable]
    class Rule
    {
        public GameObject Room;
        public Vector2Int MinPosition;
        public Vector2Int MaxPosition;

        public bool Obligatory;

        public int ProbabilityOfSpawning(int X, int Y)
        {
            // 0- Cannot spawn 1- Can Spawn 2- Has to Spawn

            if (X >= MinPosition.x && X <= MaxPosition.x && Y >= MinPosition.y && Y <= MaxPosition.y)
            {
                return Obligatory ? 2 : 1;
            }
            return 0;
        }
    }

    [SerializeField]
    Vector2Int Size;

    [SerializeField]
    Rule[] Rooms;

    [SerializeField]
    Vector2 Offset;

    [SerializeField]
    int StartingPos = 0;

    [SerializeField]
    List<Cell> Board;
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                Cell CurrentCell = Board[Mathf.FloorToInt(i + j * Size.x)];
                if (CurrentCell.Visited)
                {
                    int RandomRoom = -1;
                    List<int> AvailableRooms = new List<int>();

                    for (int k = 0; k < Rooms.Length; k++)
                    {
                        int p = Rooms[k].ProbabilityOfSpawning(i, j);

                        if(p == 2)
                        {
                            RandomRoom = k;
                            break;
                        }
                        else if(p == 1){
                            AvailableRooms.Add(k);
                        }
                    }

                    if(RandomRoom == -1)
                    {
                        if(AvailableRooms.Count > 0)
                        {
                            RandomRoom = AvailableRooms[Random.Range(0, AvailableRooms.Count)];
                        }
                        else
                        {
                            RandomRoom = 0;
                        }
                    }

                    RoomBehaviour NewRoom = Instantiate(Rooms[RandomRoom].Room, new Vector3(i * Offset.x, 0, -j * Offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    NewRoom.UpdateRoom(CurrentCell.Status);

                    NewRoom.name += " " + i + "-" + j;
                }
            }
        }
    }

    private void MazeGenerator()
    {
        Board = new List<Cell>();

        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                Board.Add(new Cell());
            }
        }

        int CurrentCell = StartingPos;

        Stack<int> Path = new Stack<int>();

        int Limit = 0;

        while(Limit < 15)
        {
            Limit++;

            Board[CurrentCell].Visited = true;

            if (CurrentCell == Board.Count - 1)
                break;

            
            // Check Cell Neighbors
            List<int> Neighbors = CheckNeighbors(CurrentCell);

            if(Neighbors.Count == 0)
            {
                if(Path.Count == 0)
                {
                    break;
                }
                else
                {
                    CurrentCell = Path.Pop();
                }
            }
            else
            {
                Path.Push(CurrentCell);

                int NewCell = Neighbors[Random.Range(0, Neighbors.Count)];

                if(NewCell > CurrentCell)
                {
                    // Down or Right
                    if(NewCell - 1 == CurrentCell)
                    {
                        //Right
                        Board[CurrentCell].Status[2] = true;
                        CurrentCell = NewCell;
                        Board[CurrentCell].Status[3] = true;
                    }
                    else
                    {
                        //Down
                        Board[CurrentCell].Status[1] = true;
                        CurrentCell = NewCell;
                        Board[CurrentCell].Status[0] = true;
                    }
                } else
                {
                    // Up or Left
                    if (NewCell + 1 == CurrentCell)
                    {
                        //Left
                        Board[CurrentCell].Status[3] = true;
                        CurrentCell = NewCell;
                        Board[CurrentCell].Status[2] = true;
                    }
                    else
                    {
                        //Up
                        Board[CurrentCell].Status[0] = true;
                        CurrentCell = NewCell;
                        Board[CurrentCell].Status[1] = true;
                    }
                }
            }
        }

        GenerateDungeon();
    }

    private List<int> CheckNeighbors(int _Cell)
    {
        List<int> Neighbors = new List<int>();

        // Check Up not Visited
        if(_Cell - Size.x >= 0 && !Board[Mathf.FloorToInt(_Cell - Size.x)].Visited)
        {
            Neighbors.Add(Mathf.FloorToInt(_Cell - Size.x));
        }

        // Check Down
        if (_Cell + Size.x < Board.Count && !Board[Mathf.FloorToInt(_Cell + Size.x)].Visited)
        {
            Neighbors.Add(Mathf.FloorToInt(_Cell + Size.x));
        }

        // Check Right
        if ((_Cell + 1) % Size.x != 0 && !Board[Mathf.FloorToInt(_Cell + 1)].Visited)
        {
            Neighbors.Add(Mathf.FloorToInt(_Cell + 1));
        }

        // Check Left
        if (_Cell % Size.x != 0 && !Board[Mathf.FloorToInt(_Cell - 1)].Visited)
        {
            Neighbors.Add(Mathf.FloorToInt(_Cell - 1));
        }
        return Neighbors;
    }
}
