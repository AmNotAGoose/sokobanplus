using UnityEngine;

public class WallTileObject : TileObject
{
    public override void AfterInitialize()
    {
        type = "wall";
        solid = true;
        pushable = false;

        order = 0;
        renderOrder = 50;
    }
}
