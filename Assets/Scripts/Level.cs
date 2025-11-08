using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Tile[,] tiles;
    public List<WinTileObject> winConditions;

    void Start()
    {
         
    }
     
    void Update()
    {
        
    }

    public bool CheckIfWon()
    {
        foreach (WinTileObject winTileObject in winConditions)
        {
            if (!winTileObject.isWon) return false;
        }
        return true;
    }
}
