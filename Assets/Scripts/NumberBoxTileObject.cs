using System.Runtime.CompilerServices;
using UnityEngine;

public class NumberBoxTileObject : TileObject
{
    public float value; // options 0

    private void Awake()
    {
        type = "numberbox";
        solid = true;
        pushable = true;

        value = float.Parse(options[0]);

        order = 20;
        renderOrder = 20;
    }

    public override float OnCommand(string command, float prev) { return prev; }
    public override void OnCommandFinished(float newValue)
    {
        value = newValue;
    }
}
