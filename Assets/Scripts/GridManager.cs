using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] int gridX = 10;
    [SerializeField] int gridY = 10;
    [SerializeField] float gridCellSize = 3f;
    [SerializeField] Vector2 gridOrigin = default;
    [SerializeField] Color lineColor = default;
    [SerializeField] Material lineMaterial = default;

    GameObject lineHolder;
    WorldGrid grid;

    private void Start()
    {
        grid = new WorldGrid(gridX, gridY, gridCellSize, gridOrigin);
        DrawGridLines();
    }

    private void DrawGridLines()
    {
        lineHolder = new GameObject("Line Holder");
        lineHolder.transform.parent = transform;

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                DrawLine(new Vector2(x, y), new Vector2(x, y + 1));
                DrawLine(new Vector2(x, y), new Vector2(x + 1, y));
            }
        }
        DrawLine(new Vector2(0, gridY), new Vector2(gridX, gridY));
        DrawLine(new Vector2(gridX, 0), new Vector2(gridX, gridY));
    }

    private void DrawLine(Vector2 startPos, Vector2 endPos)
    {
        GameObject newLine = new GameObject("Line");
        newLine.transform.parent = lineHolder.transform;

        LineRenderer lineRenderer = newLine.AddComponent<LineRenderer>();

        lineRenderer.material = lineMaterial;

        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.sortingLayerName = "Grid";

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        Vector2 startPosition = grid.GetWorldPosition(Mathf.RoundToInt(startPos.x), Mathf.RoundToInt(startPos.y));
        float xStartPos = startPosition.x;
        float yStartPos = startPosition.y;
        Vector3 startWorldPosition = new Vector3(xStartPos, yStartPos, -5f);

        Vector2 endPosition = grid.GetWorldPosition(Mathf.RoundToInt(endPos.x), Mathf.RoundToInt(endPos.y));
        float xEndPos = endPosition.x;
        float yEndPos = endPosition.y;
        Vector3 endWorldPosition = new Vector3(xEndPos, yEndPos, -5f);

        lineRenderer.SetPosition(0, startWorldPosition);
        lineRenderer.SetPosition(1, endWorldPosition);

    }
}