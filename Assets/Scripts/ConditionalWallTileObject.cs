using UnityEngine;

public class ConditionalWallTile : TileObject
{
    Level level;
    public Color permiableColor;
    public Color solidColor;

    public SpriteRenderer spriteRenderer;

    public override void AfterInitialize()
    {
        level = FindObjectsByType<Level>(FindObjectsSortMode.None)[0];
        type = "conditionalwall";
        solid = true;
        pushable = false;

        level.switchTileSubscribers.Add(this);

        order = 0;
        renderOrder = 50;
        spriteRenderer.color = solidColor;
    }

    public override void OnSwitchTileUpdated()
    {
        bool lastSolid = solid;

        bool isNearbyValid = level.CheckIfNearbyValid(this);
        solid = !isNearbyValid;
        spriteRenderer.color = isNearbyValid ? permiableColor : solidColor;

        if (solid != lastSolid && isInitialized)
        {
            parentTile.EvaluateState();
        }
    }
}
