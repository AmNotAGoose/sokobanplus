using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPos;
    public Vector2 worldPos;
    public bool isWall;
    public List<TileObject> heldObjects = new();

    public bool solid;
    public bool stopper;
    
    public void Initialize()
    {
        transform.localPosition = worldPos;
    }

    public void AddObject(TileObject newObject)
    {
        heldObjects.Add(newObject);
        heldObjects.Sort((x, y) => x.order.CompareTo(y.order));
    }
    
    public void Evaluate(string command)
    {
        if (heldObjects.Count == 0) return;

        int result = 0;
        foreach (TileObject obj in heldObjects)
        {
            result = obj.OnCommand(command, result);
        }
    }
}
