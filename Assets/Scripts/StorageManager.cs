using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    public Dictionary<string, Sprite[]> sprites = new();

    public Sprite[] GetSprite(string id)
    {
        if (!sprites.ContainsKey(id)) return sprites["unknown"];
        return sprites[id];
    }
}
