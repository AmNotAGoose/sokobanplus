using UnityEngine;

public class ConditionalWallTile : TileObject
{
    public override void AfterInitialize()
    {
        type = "conditionalwall";
        solid = true;
        pushable = false;

        order = 0;
        renderOrder = 50;
    }

    public override void OnCommandFinished(float newValue)
    {
        
    }
}
