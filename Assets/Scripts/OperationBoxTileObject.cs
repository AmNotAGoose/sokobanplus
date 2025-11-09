using Unity.VisualScripting;
using UnityEngine;

public class OperationBoxTileObject : TileObject
{
    string operation; // options 0
    float value; // options 1

    public override void AfterInitialize()
    {
        type = "operationbox";
        solid = false;
        pushable = false;

        operation = options[0];
        value = float.Parse(options[1]);

        order = 0;
        renderOrder = 50;
    }

    public override float OnCommand(string command, float prev)
    {
        return operation switch
        {
            "+" => prev + value,
            "-" => prev - value,
            "*" => prev * value,
            "/" => prev / value,
            _ => prev,
        };
    }

    public override void OnCommandFinished(float newValue) { }
}
