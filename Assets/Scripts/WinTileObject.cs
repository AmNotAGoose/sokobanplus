using UnityEngine;

public class WinTileObject : TileObject
{
    Level level;

    public float targetValue; // options 0
    public bool isWon;

    public override void AfterInitialize()
    {
        level = FindObjectsByType<Level>(FindObjectsSortMode.None)[0];
        level.winConditions.Add(this);

        targetValue = float.Parse(options[0]);

        type = "win";
        solid = false;
        pushable = false;

        order = 30;
        renderOrder = 30;
    }

    public override float OnCommand(string command, float prev) { return prev; }

    public override void OnCommandFinished(float newValue)
    {
        isWon = newValue == targetValue;
    }
}
