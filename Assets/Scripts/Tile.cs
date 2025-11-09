using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HeldObjectsFiltering
{
    None,
    Pushable,
}

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

    public void AddObjects(List<TileObject> tileObjects)
    {
        foreach (TileObject tileObject in tileObjects)
        {
            heldObjects.Add(tileObject);
        }

        heldObjects.Sort((x, y) => x.order.CompareTo(y.order));
    }

    public List<TileObject> PopObjects(HeldObjectsFiltering filter)
    {
        List<TileObject> returnedObjects = new(heldObjects);
        switch (filter)
        {
            case HeldObjectsFiltering.Pushable:
                heldObjects = heldObjects.Where(obj => !obj.pushable).ToList();
                return returnedObjects.Where(obj => obj.pushable).ToList();
            case HeldObjectsFiltering.None:
                heldObjects = new();
                return returnedObjects;
            default:
                return new List<TileObject> { };
        }
    }
    
    public void Evaluate(string command)
    {
        if (heldObjects.Count == 0) return;

        int result = 0;
        foreach (TileObject obj in heldObjects)
        {
            result = obj.OnCommand(command, result);
        }

        solid = heldObjects.Any(obj => obj.solid);
        stopper = heldObjects.Any(obj => obj.solid && !obj.pushable);
    }
}
