using System;
using System.Collections.Generic;
using UnityEngine;

public class GridParser
{
    public struct ParsedGrid
    {
        public int width;
        public int height;
        public bool[,] walls;
        public List<GridObject> objects;
    }

    public struct GridObject
    {
        public string type;
        public int x;
        public int y;
    }

    /// <summary>
    /// Parses a string in the format: What is this diddy blud doing on the calculato 
    /// x|y=|haswall|haswall|...|=|object_type-x,y|object_type-x,y|...
    /// </summary>
    public static ParsedGrid ParseGridString(string data)
    {
        var parsed = new ParsedGrid();
        parsed.objects = new List<GridObject>();

        string[] mainParts = data.Split(new string[] { "=|" }, StringSplitOptions.None);

        if (mainParts.Length < 3)
            throw new Exception("Invalid grid format.");

        // 1. Parse size
        string[] sizeParts = mainParts[0].Split('|');
        parsed.width = int.Parse(sizeParts[0]);
        parsed.height = int.Parse(sizeParts[1]);

        // 2. Parse wall booleans
        string[] wallParts = mainParts[1].Split('|', StringSplitOptions.RemoveEmptyEntries);
        if (wallParts.Length != parsed.width * parsed.height)
            throw new Exception("Wall count does not match grid size.");

        parsed.walls = new bool[parsed.width, parsed.height];

        for (int i = 0; i < wallParts.Length; i++)
        {
            int x = i % parsed.width;
            int y = i / parsed.width;
            parsed.walls[x, y] = bool.Parse(wallParts[i]);
        }

        // 3. Parse objects
        string[] objectParts = mainParts[2].Split('|', StringSplitOptions.RemoveEmptyEntries);
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
