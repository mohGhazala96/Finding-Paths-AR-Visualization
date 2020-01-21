using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchVisualizer : MonoBehaviour
{
    public PathFinding pathFinder;

    public void UpdateVisual()
    {
        GridMaker gridMaker = pathFinder.gridMaker;
        for (int i = 0; i < gridMaker.rows; i++)
        {
            for (int j = 0; j < gridMaker.coloumns; j++)
            {
                GameObject gridCell = gridMaker.gridCells[i, j];
                if (pathFinder.searchMethod.Equals(SearchMethod.AStar))
                {
                    gridCell.GetComponent<NodeVisual>().UpdateCosts(gridMaker.grid.cells[i, j]);
                }else if (pathFinder.searchMethod.Equals(SearchMethod.Dijkstra))
                {
                    gridCell.GetComponent<NodeVisual>().UpdateWeight(gridMaker.grid.cells[i, j]);
                }

            }
        }
    }
    public void SetCellToRed(Cell cell)
    {
        UpdateVisual();
        GridMaker gridMaker = pathFinder.gridMaker;
        GameObject gridCell = gridMaker.gridCells[cell.x, cell.y];
        gridCell.GetComponent<NodeVisual>().SetColorRed();
    }
    public void SetCellToBlue(Cell cell)
    {
        UpdateVisual();
        GridMaker gridMaker = pathFinder.gridMaker;
        GameObject gridCell = gridMaker.gridCells[cell.x, cell.y];
        gridCell.GetComponent<NodeVisual>().SetColorBlue();
    }
    public void SetCellToGreen(Cell cell)
    {
        UpdateVisual();
        GridMaker gridMaker = pathFinder.gridMaker;
        GameObject gridCell = gridMaker.gridCells[cell.x, cell.y];
        gridCell.GetComponent<NodeVisual>().SetColorGreen();


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
