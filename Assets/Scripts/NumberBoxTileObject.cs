using UnityEngine;

public class NumberBoxTileObject : TileObject
{
    public int value;

    private void Awake()
    {
        type = "numberbox";
        solid = true;
        pushable = true;
        renderOrder = 20;
    }

    public override int OnCommand(string command, int prev)
    {
        return prev;
    }
    public override void OnPlayerEnter() { }
    public override void OnPlayerExit() { }
}
