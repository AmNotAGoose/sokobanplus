using UnityEngine;

public class WallTileObject : TileObject
{
    private void Awake()
    {
        type = "wall";
        solid = true;
        pushable = false;
        renderOrder = 50;
    }

    public override int OnCommand(string command, int prev)
    {
        return prev;
    }
    public override void OnPlayerEnter() { }
    public override void OnPlayerExit() { }
}
