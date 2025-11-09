using System.Runtime.CompilerServices;
using UnityEngine;

public class NumberBoxTileObject : TileObject
{
    public int value;

    private void Awake()
    {
        type = "numberbox";
        solid = true;
        pushable = true;

        order = 20;
        renderOrder = 20;
    }

    public override int OnCommand(string command, int prev) { return prev; }
    public override void OnCommandFinished(int newValue)
    {
        value = newValue;
    }
}
