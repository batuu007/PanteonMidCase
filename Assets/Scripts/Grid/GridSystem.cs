using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : Singleton<GridSystem>
{

    [SerializeField] public LayerMask unwalkableMask;
    [SerializeField] public Vector2 gridWorldSize;

    [SerializeField] public float nodeRadius;

    private PathNode[,] _grid;

    private float _nodeDiamater;

    private int _gridSizeX, _gridSizeY;

    private void Awake()
    {
        _nodeDiamater = nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / _nodeDiamater);
        _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / _nodeDiamater);
        CreateGrid();
    }
    public int MaxSize
    {
        get { return _gridSizeX * _gridSizeY; }
    }
    bool walkable;
    Vector3 worldPoint;

    public void CreateGrid()
    {
        _grid = new PathNode[_gridSizeX, _gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiamater + nodeRadius) + Vector3.up * (y * _nodeDiamater + nodeRadius);
                walkable = !Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask);

                _grid[x, y] = new PathNode(walkable, worldPoint, x, y);
            }
        }
    }
    public PathNode NodeFromWorldPoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
        return _grid[x, y];
    }
    public List<PathNode> GetNeibours(PathNode node)
    {
        List<PathNode> neibours = new List<PathNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                {
                    neibours.Add(_grid[checkX, checkY]);
                }
            }

        }
        return neibours;
    }
    public void UpdateGrid(Vector3 worldPos, int x, int y)
    {
        Vector3 min = new Vector3(worldPos.x - (x * nodeRadius), worldPos.y - (y * nodeRadius));
        Vector3 max = new Vector3(worldPos.x + (x * nodeRadius), worldPos.y + (y * nodeRadius));
        for (float i = min.x; i < max.x; i += nodeRadius)
        {
            for (float j = min.y; j < max.y; j += nodeRadius)
            {
                NodeFromWorldPoint(new Vector3(i, j)).isWalkable = false;
            }
        }
    }
}
