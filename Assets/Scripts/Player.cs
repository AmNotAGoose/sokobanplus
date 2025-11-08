using UnityEngine;

public class Player : TileObject
{
    public override int OnCommand(string command, int prev) { return prev; }
    public override void OnPlayerEnter() { }
    public override void OnPlayerExit() { }
}
