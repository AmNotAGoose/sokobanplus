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
        newObject.SetNewParentTile(this);
        EvaluateState();
    }

    public void AddObjects(List<TileObject> tileObjects)
    {
        foreach (TileObject tileObject in tileObjects)
        {
            AddObject(tileObject);
        }
    }

    public List<TileObject> PopObjects(HeldObjectsFiltering filter)
    {
        List<TileObject> returnedObjects = new(heldObjects);
        switch (filter)
        {
            case HeldObjectsFiltering.Pushable:
                heldObjects = heldObjects.Where(obj => !obj.pushable).ToList();
                returnedObjects = returnedObjects.Where(obj => obj.pushable).ToList();
                break;
            case HeldObjectsFiltering.None:
                heldObjects = new();
                break;
            default:
                returnedObjects = new List<TileObject> { };
                break;
        }

        EvaluateState();
        return returnedObjects;
    }

    public void Evaluate(string command)
    {
        EvaluateCommand(command);
        EvaluateState();
    }

    // this is SO BAD IT LITERALLY LOOPS OVER 10 TIMES PER TILE 
    public void EvaluateCommand(string command)
    {
        if (heldObjects.Count == 0) return;
        heldObjects.Sort((x, y) => x.order.CompareTo(y.order)); // asc!!!

        float result = 0;
        foreach (TileObject obj in heldObjects)
        {
            result = obj.OnCommand(command, result);
        }

        foreach (TileObject obj in heldObjects)
        { 
            obj.OnCommandFinished(result);
        }

        EvaluateState();
    }

    public void EvaluateState()
    {
        solid = heldObjects.Any(obj => obj.solid);
        stopper = heldObjects.Any(obj => obj.solid && !obj.pushable);

        float result = 0;
        foreach (TileObject obj in heldObjects)
        {
            result = obj.OnCommand("PASS", result);
        }

        foreach (TileObject obj in heldObjects)
        {
            obj.OnCommandFinished(result);
        }
    }
}
