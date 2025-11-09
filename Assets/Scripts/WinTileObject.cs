using UnityEngine;

public class WinTileObject : TileObject
{
    Level level;

    public int targetValue;
    public bool isWon;

    private void Awake()
    {
        level = FindObjectsByType<Level>(FindObjectsSortMode.None)[0];
        level.winConditions.Add(this);

        type = "win";
        solid = false;
        pushable = false;

        order = 30;
        renderOrder = 30;
    }

    public override int OnCommand(string command, int prev) { return prev; }

    public override void OnCommandFinished(int newValue)
    {
        isWon = newValue == targetValue;
    }
}
