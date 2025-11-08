using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteEntry
{
    public string id;
    public Sprite[] sprites;
}

public class StorageManager : MonoBehaviour
{
    public List<SpriteEntry> spriteList = new();

    private Dictionary<string, Sprite[]> sprites;

    void Awake()
    {
        sprites = new Dictionary<string, Sprite[]>();
        foreach (var entry in spriteList)
        {
            sprites[entry.id] = entry.sprites;
        }

        if (!sprites.ContainsKey("unknown")) sprites["unknown"] = new Sprite[0];
    }

    public Sprite[] GetSprite(string id)
    {
        if (!sprites.ContainsKey(id)) return sprites["unknown"];
        return sprites[id];
    }
}
