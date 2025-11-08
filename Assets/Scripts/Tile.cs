using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPos;
    public bool isWall;
    public List<TileObject> heldObjects = new List<TileObject>();

    public void AddObject(TileObject newObject)
    {
        heldObjects.Add(newObject);
        heldObjects.Sort((x, y) => x.order.CompareTo(y.order));
    }

    public void Evaluate(string command)
    {
        int result = 0;
        foreach (TileObject obj in heldObjects)
        {
            result = obj.OnCommand(command, result);
        }
    }
}
