using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TileObjectPrefab
{
    public string id;
    public TileObject tileObject;
}

public class Level : MonoBehaviour
{
    public string levelString;

    public int width;
    public int height;
    public Tile[,] grid;
    public List<WinTileObject> winConditions;

    public Transform gridParent;
    public float tileSpacing = 0f;
    public GameObject tilePrefab;
    public List<TileObjectPrefab> tileObjectPrefabs;

    [Header("LEVEL SPECIFIC SETTINGS")]
    public Color backgroundColor;
    public ParticleSystem[] winParticles;
    public AudioClip winSound;


    void Start()
    {
        GridParser.ParsedGrid parsedGrid = GridParser.ParseGridString(levelString);
        width = parsedGrid.width;
        height = parsedGrid.height;

        if (width == 0 || height == 0) return;
        grid = new Tile[width, height];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                Vector3 origin = new Vector3(-(width - 1) / 2f, -(height - 1) / 2f, 0f);
                Vector3 localPos = origin + new Vector3(i, j, 0f) * (1f + tileSpacing);

                GameObject curTileGO = Instantiate(tilePrefab, gridParent);

                Tile curTile = curTileGO.GetComponent<Tile>();
                curTile.gridPos = new Vector2Int(i, j);
                curTile.worldPos = localPos;

                grid[i, j] = curTile;
                curTile.Initialize();
            }
        }

        foreach (GridParser.GridObject gridObject in parsedGrid.objects)
        {
            GameObject tileObjectPrefab = tileObjectPrefabs.First(obj => obj.id == gridObject.type).tileObject.gameObject;
            GameObject objGO = Instantiate(tileObjectPrefab, grid[gridObject.x, gridObject.y].transform);

            TileObject tileObject = objGO.GetComponent<TileObject>();

            tileObject.gridPos = new Vector2Int(gridObject.x, gridObject.y);
            grid[gridObject.x, gridObject.y].AddObject(tileObject);
            tileObject.Initialize(gridObject.options);
        }

        Camera cam = Camera.main;
        cam.backgroundColor = backgroundColor;
        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;

        float padding = 0.9f;

        float gridWidth = width + tileSpacing * (width - 1);
        float gridHeight = height + tileSpacing * (height - 1);

        float scale = Mathf.Min(screenWidth / gridWidth, screenHeight / gridHeight) * padding;
        gridParent.localScale = new Vector3(scale, scale, 1f);
    }

    public void CheckIfWon()
    {
        if (!winConditions.All(w => w.isWon)) return;


    }

    public bool InGridBounds(Vector2Int selectedPos)
    {
        return selectedPos.x >= 0 && selectedPos.y >= 0 && selectedPos.x < grid.GetLength(0) && selectedPos.y < grid.GetLength(1);
    }

    public void MoveObject(TileObject tileObj, Vector2Int direction)
    {
        Vector2Int curGridPos = tileObj.gridPos;
        
        bool canMove = false;
        bool hasEmptySpace = false;
        List<Tile> selectedTiles = new() { grid[curGridPos.x, curGridPos.y] };
        Vector2Int selectedPos = curGridPos;
        while (true)
        {
            selectedPos += direction;
            if (!InGridBounds(selectedPos)) break;

            Tile selectedTile = grid[selectedPos.x, selectedPos.y];
            if (selectedTile.stopper) break;
            if (!selectedTile.solid)
            {
                hasEmptySpace = true;
                break;
            }

            selectedTiles.Add(selectedTile);
        }

        if (hasEmptySpace) canMove = true;
        if (!canMove) return;

        selectedTiles.Reverse();
        foreach (Tile curTile in selectedTiles)
        {
            List<TileObject> curTileObjects = curTile.PopObjects(HeldObjectsFiltering.Pushable);
            Vector2Int nextTile = curTile.gridPos + direction;
            grid[nextTile.x, nextTile.y].AddObjects(curTileObjects);
        }

        CheckIfWon();
    }

    public void EvaluateCommand(string command)
    {
        foreach (Tile tile in grid)
        {
            tile.Evaluate(command);
        }
    }
}
