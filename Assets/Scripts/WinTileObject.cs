using UnityEngine;

public class WinTileObject : TileObject
{
    public TileObjectTextUpdater textManager;
    Level level;

    public Animator animator;

    public float targetValue; // options 0
    public bool isWon;

    public override void AfterInitialize()
    {
        level = FindObjectsByType<Level>(FindObjectsSortMode.None)[0];
        level.winConditions.Add(this);

        targetValue = float.Parse(options[0]);
        textManager.UpdateText(0, targetValue.ToString());

        type = "win";
        solid = false;
        pushable = false;

        order = 30;
        renderOrder = 30;

        animator.Play("WinningAnimation");
        animator.speed = isWon ? 1 : 0;
    }

    public override void OnCommandFinished(float newValue)
    {
        isWon = newValue == targetValue;
        animator.speed = isWon ? 1 : 0;
    }
}
