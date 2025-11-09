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
        renderOrder = 30;
    }

    public override int OnCommand(string command, int prev)
    {
        if (prev == targetValue)
        {
            isWon = true;
        } else
        {
            isWon = false;
        }
        
        return prev;
    }
    public override void OnPlayerEnter() { }
    public override void OnPlayerExit() { }
}
