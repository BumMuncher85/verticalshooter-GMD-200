using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    [Header("Tilemaps")]
    [SerializeField] private Tilemap TM_floor;
    [SerializeField] private Tilemap TM_water;
    [SerializeField] private Tilemap TM_decorCollision;

    [Header("Tiles")]
    [SerializeField] private AnimatedTile T_water;
    [SerializeField] private Tile T_floorEdgeLeft;
    [SerializeField] private Tile T_floorEdgeRight;
    [SerializeField] private Tile T_basicFloor;
    [SerializeField] private Tile T_fence;

    [Header("Other")]
    [SerializeField] private GameObject player;

    private int PATH_LEFTBOUND = -6;
    private int PATH_RIGHTBOUND = 6;
    private int WATER_LEFTBOUND = -13;
    private int WATER_RIGHTBOUND = 13;
    private int PLAYER_TOPBOUND = 5;
    private int PLAYER_BOTTOMBOUND = -5;

    private Vector3Int startingCell;
    private Vector3Int currentCell;

    Dictionary<int,bool> rowRendered = new Dictionary<int,bool>();

    // Start is called before the first frame update
    void Start()
    {
        startingCell = TM_water.WorldToCell(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        currentCell = TM_water.WorldToCell(player.transform.position);
        int topRender = PLAYER_TOPBOUND + currentCell.y;
        int bottomRender = PLAYER_BOTTOMBOUND + currentCell.y;
        for (int y = bottomRender; y <= topRender; y++)
        {
            if(!rowRendered.ContainsKey(y))
            {
                rowRendered[y] = true;
                for (int w = WATER_LEFTBOUND; w <= WATER_RIGHTBOUND; w++)
                {
                    Vector3Int cell = startingCell + new Vector3Int(w, y, 0);
                    if(w < PATH_LEFTBOUND + 1 || w > PATH_RIGHTBOUND - 1)
                    {
                        TM_water.SetTile(cell, T_water);
                    }
                    
                }
                for (int x = PATH_LEFTBOUND; x <= PATH_RIGHTBOUND; x++)
                {
                    Vector3Int cell = startingCell + new Vector3Int(x, y, 0);
                    if (x == PATH_LEFTBOUND)
                    {
                        TM_floor.SetTile(cell, T_floorEdgeLeft);
                    }
                    else if (x == PATH_RIGHTBOUND)
                    {
                        TM_floor.SetTile(cell, T_floorEdgeRight);
                    }
                    else
                    {
                        TM_floor.SetTile(cell, T_basicFloor);
                        if(x == PATH_LEFTBOUND + 1)
                        {
                            TM_decorCollision.SetTile(cell, T_fence);
                        }
                        if(x == PATH_RIGHTBOUND - 1)
                        {
                            TM_decorCollision.SetTile(cell, T_fence);
                        }
                    }
                }
            }
        }
    }
}
