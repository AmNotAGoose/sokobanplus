using UnityEngine;

public interface ISwitchTileObject
{
    bool isSatisfied { get; }
}

public class SwitchTileObject : TileObject, ISwitchTileObject
{
    public TileObjectTextUpdater textManager;
    Level level;

    public Animator animator;

    public float targetValue; // options 0
    public int id; // options 1
    public bool isSatisfied { get; private set; }

    public override void AfterInitialize()
    {
        level = FindObjectsByType<Level>(FindObjectsSortMode.None)[0];
        targetValue = float.Parse(options[0]);
        id = int.Parse(options[1]);
        level.switchTiles[id] = this;

        textManager.UpdateText(0, targetValue.ToString());

        type = "switch";
        solid = false;
        pushable = false;

        order = 30;
        renderOrder = 30;

        animator.Play("WinningAnimation");
        animator.speed = isSatisfied ? 1 : 0;
    }

    public override void OnCommandFinished(float newValue)
    {
        isSatisfied = newValue == targetValue;
        animator.speed = isSatisfied ? 1 : 0;
    }
}
