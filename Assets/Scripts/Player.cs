using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : TileObject
{
    public Level level;

    public bool typing;
    public PlayerCommands playerCommands;

    public bool canMove;

    private void Awake()
    {
        playerCommands = transform.AddComponent<PlayerCommands>();
        level = FindObjectsByType<Level>(FindObjectsSortMode.None)[0];

        type = "player";
        solid = true;
        pushable = true;

        order = 10;
        renderOrder = 20;
    }

    private void Update()
    {
        typing = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerCommands.ClearText();
        }

        if (typing)
        {
            playerCommands.AddText(Input.inputString);
        } else
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
    }

    public IEnumerator ResetCanMove()
    {
        if (canMove) yield return null;

        yield return new WaitForSeconds(0.5f);
        canMove = true;
    }

    public override int OnCommand(string command, int prev) { return prev; }
    public override void OnCommandFinished(int newValue) { }
}
