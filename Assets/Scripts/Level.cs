using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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
    public Dictionary<int, SwitchTileObject> switchTiles = new();

    public Transform gridParent;
    public float tileSpacing = 0f;
    public GameObject tilePrefab;
    public List<TileObjectPrefab> tileObjectPrefabs;

    [Header("LEVEL SPECIFIC SETTINGS")]
    public Color backgroundColor;
    public Color tileColor;
    public ParticleSystem[] winParticlesStart;
    public ParticleSystem[] winParticlesStop; 
    private AudioManager audioManager;

    public Volume volume;
    private LensDistortion lensDistortion;

    public bool ending = false;

    public int nextLevel;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();

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

                curTile.transform.GetComponent<SpriteRenderer>().color = tileColor;

                grid[i, j] = curTile;
                curTile.Initialize();
            }
        }

        Camera cam = Camera.main;
        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;

        float padding = 0.9f;

        float gridWidth = width + tileSpacing * (width - 1);
        float gridHeight = height + tileSpacing * (height - 1);

        float scale = Mathf.Min(screenWidth / gridWidth, screenHeight / gridHeight) * padding;
        gridParent.localScale = new Vector3(scale, scale, 1f);

        foreach (GridParser.GridObject gridObject in parsedGrid.objects)
        {
            GameObject tileObjectPrefab = tileObjectPrefabs.First(obj => obj.id == gridObject.type).tileObject.gameObject;
            GameObject objGO = Instantiate(tileObjectPrefab, grid[gridObject.x, gridObject.y].transform);

            TileObject tileObject = objGO.GetComponent<TileObject>();

            tileObject.gridPos = new Vector2Int(gridObject.x, gridObject.y);
            grid[gridObject.x, gridObject.y].AddObject(tileObject);
            tileObject.Initialize(gridObject.options);
        }

        cam.backgroundColor = backgroundColor;
        StartCoroutine(StartGameEffect());
    }

    public IEnumerator StartGameEffect()
    {
        if (volume.profile.TryGet<LensDistortion>(out lensDistortion)) lensDistortion.active = true;

        float startValue = lensDistortion.intensity.value;
        float elapsed = 0f;
        float duration = 2f;
        float targetValue = 0.188f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            lensDistortion.intensity.value = Mathf.Lerp(startValue, targetValue, t);
            yield return null;
        }

        lensDistortion.intensity.value = targetValue;
    }

    public bool CheckIfNearbyValid(TileObject tileObject)
    {
        List<Vector2Int> nearbyTiles = new();
        nearbyTiles.Add(tileObject.gridPos + new Vector2Int(-1, 1));
        nearbyTiles.Add(tileObject.gridPos + new Vector2Int(-1, 0));
        nearbyTiles.Add(tileObject.gridPos + new Vector2Int(-1, -1));

        nearbyTiles.Add(tileObject.gridPos + new Vector2Int(0, 1));
        nearbyTiles.Add(tileObject.gridPos + new Vector2Int(0, 0));
        nearbyTiles.Add(tileObject.gridPos + new Vector2Int(0, -1));

        nearbyTiles.Add(tileObject.gridPos + new Vector2Int(1, 1));
        nearbyTiles.Add(tileObject.gridPos + new Vector2Int(1, 0));
        nearbyTiles.Add(tileObject.gridPos + new Vector2Int(1, -1));

        bool isValid = true;
        bool oneFound = false;
        foreach (Vector2Int tilePos in nearbyTiles)
        {
            if (!InGridBounds(tilePos)) continue;
            foreach (TileObject curTileObject in grid[tilePos.x, tilePos.y].heldObjects)
            {
                if (curTileObject is ISwitchTileObject switchObject)
                {
                    oneFound = true;
                    isValid = isValid && switchObject.isSatisfied;
                }
            }
        }

        isValid = isValid && oneFound;

        return isValid;
    }

    public IEnumerator CheckIfWon()
    {
        if (!winConditions.All(w => w.isWon)) yield break;

        ending = true;

        foreach (ParticleSystem particle in winParticlesStart)
        {
            particle.Play();
        }

        foreach (ParticleSystem particle in winParticlesStop)
        {
            particle.Pause();
        }

        audioManager.PlaySound("sfx:win", false);

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextLevel);
    }

    public bool InGridBounds(Vector2Int selectedPos)
    {
        return selectedPos.x >= 0 && selectedPos.y >= 0 && selectedPos.x < grid.GetLength(0) && selectedPos.y < grid.GetLength(1);
    }

    public void MoveObject(TileObject tileObj, Vector2Int direction)
    {
        if (ending) return;

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

        StartCoroutine(CheckIfWon());
    }

    public void EvaluateCommand(string command)
    {
        foreach (Tile tile in grid)
        {
            tile.Evaluate(command);
        }
    }
}
