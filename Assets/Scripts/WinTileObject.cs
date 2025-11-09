using UnityEngine;

public class WinTileObject : TileObject
{
    public int targetValue;
    public bool isWon;

    private void Awake()
    {
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
