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

        order = 0;
        renderOrder = 50;
    }

    public override void OnEvaluateFinish()
    {
        bool isNearbyValid = level.CheckIfNearbyValid(this);
        solid = !isNearbyValid;
        spriteRenderer.color = isNearbyValid ? permiableColor : solidColor;
    }
}
