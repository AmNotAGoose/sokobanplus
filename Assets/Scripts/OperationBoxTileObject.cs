using Unity.VisualScripting;
using UnityEngine;

public class OperationBoxTileObject : TileObject
{
    public TileObjectTextUpdater textManager;

    public string operation; // options 0
    public float value; // options 1

    public override void AfterInitialize()
    {
        type = "operationbox";
        solid = false;
        pushable = false;

        //operation = options[0];
        value = float.Parse(options[0]);
        textManager.UpdateText(0, value.ToString());

        order = 50;
        renderOrder = 50;
    }

    public override float OnCommand(string command, float prev)
    {
        return command switch
        {
            "ADD" => prev + value,
            "SUB" => prev - value,
            "MUL" => prev * value,
            "DIV" => prev / value,
            _ => prev,
        };
    }

    public override void OnCommandFinished(float newValue) { }
}
