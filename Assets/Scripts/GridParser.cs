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
        public List<string> options;
    }

    /// <summary>
    /// Format: width|height=|type-x,y[,option1,option2,...]|...
    /// Example: 4|8=|player-0,0,speed,blue|wall-1,1|box-2,3,heavy
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
            string[] parts = typeAndPos[1].Split(',');
            if (parts.Length < 2) continue;

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);

            // all remaining commas = options
            List<string> options = new List<string>();
            if (parts.Length > 2)
            {
                for (int i = 2; i < parts.Length; i++)
                    options.Add(parts[i]);
            }

            parsed.objects.Add(new GridObject
            {
                type = type,
                x = x,
                y = y,
                options = options
            });
        }

        return parsed;
    }
}
