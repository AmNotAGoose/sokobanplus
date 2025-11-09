using System;
using System.Collections.Generic;
using UnityEngine;

public class GridParser
{
    public struct ParsedGrid
    {
        public int width;
        public int height;
        public List<GridObject> objects;
    }

    public struct GridObject
    {
        public string type;
        public int x;
        public int y;
    }

    /// <summary>
    /// Parses a string in the format:
    /// width|height=|type-x,y|type-x,y|...
    /// Example: 4|8=|player-0,0|win-2,3|numberbox-1,2|wall-0,1
    /// </summary>
    public static ParsedGrid ParseGridString(string data)
    {
        var parsed = new ParsedGrid();
        parsed.objects = new List<GridObject>();

        string[] mainParts = data.Split(new string[] { "=|" }, StringSplitOptions.None);

        if (mainParts.Length < 2)
            throw new Exception("Invalid grid format.");

        // 1. Parse size
        string[] sizeParts = mainParts[0].Split('|');
        parsed.width = int.Parse(sizeParts[0]);
        parsed.height = int.Parse(sizeParts[1]);

        // 2. Parse objects (walls are just objects now)
        string[] objectParts = mainParts[1].Split('|', StringSplitOptions.RemoveEmptyEntries);
        foreach (string obj in objectParts)
        {
            string[] typeAndPos = obj.Split('-');
            if (typeAndPos.Length != 2) continue;

            string type = typeAndPos[0];
            string[] xy = typeAndPos[1].Split(',');
            if (xy.Length != 2) continue;

            parsed.objects.Add(new GridObject
            {
                type = type,
                x = int.Parse(xy[0]),
                y = int.Parse(xy[1])
            });
        }

        return parsed;
    }
}
