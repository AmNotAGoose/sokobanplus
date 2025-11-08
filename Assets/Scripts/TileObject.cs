using UnityEngine;

public abstract class TileObject : MonoBehaviour
{
    public Vector2Int gridPos;
    public string type;
    public int order;
    public int renderOrder;

    public void Initialize()
    {
        transform.localPosition = Vector2.zero;
    }
    public abstract void OnPlayerEnter();
    public abstract void OnPlayerExit();
    public abstract int OnCommand(string command, int prev);
}
