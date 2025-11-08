using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string levelString;
    
    public int width;
    public int height;
    public Tile[,] grid;
    public List<WinTileObject> winConditions;

    void Start()
    {
        GridParserParsedGrid
        GridParser.ParseGridString(levelString);

        if (width == 0 || height == 0) return;
        grid = new Tile[width, height];
        
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Tile curTile = new()
                {
                    gridPos = new Vector2(i, j)
                };
                grid[i, j] = curTile;
            }
        }
    }
     
    void Update()
    {
        
    } 

    public bool CheckIfWon()
    {
        return winConditions.All(w => w.isWon);
    }
}
