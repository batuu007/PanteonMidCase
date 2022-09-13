using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : IStackItem<PathNode>
{
    [SerializeField] public bool isWalkable;
    [SerializeField] public Vector3 worldPosition;
    [SerializeField] public int gridX, gridY;
    [SerializeField] public int gCost, hCost;

    private int _stackIndex;

    public int FCost { get { return hCost + gCost; } }

    public int StackIndex { get => _stackIndex; set => _stackIndex = value; }

    public PathNode parent;
    public PathNode(bool isWalkable, Vector3 worldPosition, int _gridX, int _gridY)
    {
        this.isWalkable = isWalkable;
        this.worldPosition = worldPosition;
        this.gridX = _gridX;
        this.gridY = _gridY;
    }

    public int CompareTo(PathNode other)
    {
        int compare = FCost.CompareTo(other.FCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);
        }
        return -compare;
    }
}
