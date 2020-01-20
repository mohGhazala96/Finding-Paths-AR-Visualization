using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isObstacle;
    public Cell cameFromNode;

    public Cell(int x, int y,bool isObstacle)
    {
        this.x = x;
        this.y = y;
        this.isObstacle = isObstacle;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x + "," + y;
    }

}
