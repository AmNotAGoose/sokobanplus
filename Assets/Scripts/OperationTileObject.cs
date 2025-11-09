using UnityEngine;

public class OperationTileObject : TileObject
{
    string operation;
    string value;

    private void Awake()
    {
        type = "operation";
        solid = true;
        pushable = false;

        order = 0;
        renderOrder = 50;
    }

    public override int OnCommand(string command, int prev) { return prev; }
    public override void OnCommandFinished(int newValue) { }
}
