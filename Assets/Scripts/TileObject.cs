using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class TileObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Tile parentTile;

    public List<string> options;

    public Vector2Int gridPos;
    public string type;
    public int order;
    public int renderOrder;

    public bool solid;
    public bool pushable;

    private Queue<Vector3> moveQueue = new Queue<Vector3>();
    private bool isMoving = false;

    public void Initialize(List<string> _options)
    {
        options = _options;
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localPosition = Vector2.zero;
        spriteRenderer.sortingOrder = renderOrder;
        AfterInitialize();
        parentTile.EvaluateState();
    }
    public abstract void AfterInitialize();
    public void SetNewParentTile(Tile tile)
    {
        parentTile = tile;
        gridPos = tile.gridPos;
        transform.SetParent(tile.transform, true);

        moveQueue.Enqueue(tile.transform.TransformPoint(Vector2.zero));
        if (!isMoving) StartCoroutine(ProcessMoveQueue());

        parentTile.EvaluateState();
    }
    IEnumerator ProcessMoveQueue()
    {
        isMoving = true;

        while (moveQueue.Count > 0)
        {
            Vector3 target = moveQueue.Dequeue();
            yield return StartCoroutine(MoveToTarget(target, 0.1f));
        }

        isMoving = false;
    }
    IEnumerator MoveToTarget(Vector3 endPos, float duration)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        print("complete");  

        transform.position = endPos;
    }
    public virtual float OnCommand(string command, float prev) { return prev; }
    public virtual void OnCommandFinished(float newValue) { }
    public virtual void OnEvaluateFinish() { }
}
