using System.Collections;
using UnityEngine;

public class Player : TileObject
{
    public Level level;

    public bool canMove;

    public override int OnCommand(string command, int prev) { return prev; }
    public override void OnPlayerEnter() { }
    public override void OnPlayerExit() { }

    private void Awake()
    {
        type = "player";
        level = FindObjectsByType<Level>(FindObjectsSortMode.None)[0];
        solid = true;
        pushable = true;

        renderOrder = 20;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            level.MoveObject(this, new Vector2Int(0, 1));
        } 
        else if (Input.GetKeyDown(KeyCode.A))
        {
            level.MoveObject(this, new Vector2Int(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            level.MoveObject(this, new Vector2Int(0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            level.MoveObject(this, new Vector2Int(1, 0));
        }
    }

    public IEnumerator ResetCanMove()
    {
        if (canMove) yield return null;

        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }
}
