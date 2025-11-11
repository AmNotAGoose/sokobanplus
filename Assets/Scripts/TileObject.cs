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

    public bool moving;

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
        transform.SetParent(tile.transform);
        LerpToLocalOrigin();
        parentTile.EvaluateState();
    }
    public void LerpToLocalOrigin()
    {
        StartCoroutine(MoveToTarget(new Vector2(0, 0), 0.1f));
    }
    IEnumerator MoveToTarget(Vector3 endPos, float duration)
    {
        Vector3 startPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        transform.localPosition = endPos;
    }
    public virtual float OnCommand(string command, float prev) { return prev; }
    public virtual void OnCommandFinished(float newValue) { }
}
