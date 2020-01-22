using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchVisualizer : MonoBehaviour
{
    public PathFinding pathFinder;
    public float delayStep;
    void UpdateVisual(GameObject gridCell,Cell cell)
    {

        if (pathFinder.searchMethod.Equals(SearchMethod.AStar))
        {
            gridCell.GetComponent<NodeVisual>().UpdateCosts(cell);
        }
        else if (pathFinder.searchMethod.Equals(SearchMethod.Dijkstra))
        {
            gridCell.GetComponent<NodeVisual>().UpdateWeight(cell);
        }

    }
    public IEnumerator SetCellToRed(Cell cell)
    {
        yield return new WaitForSecondsRealtime(delayStep);
        GridMaker gridMaker = pathFinder.gridMaker;
        GameObject gridCell = gridMaker.gridCells[cell.x, cell.y];
        UpdateVisual(gridCell, cell);
        gridCell.GetComponent<NodeVisual>().SetColorRed();
    }
    public IEnumerator SetCellToBlue(Cell cell)
    {
        yield return new WaitForSecondsRealtime(delayStep);

        GridMaker gridMaker = pathFinder.gridMaker;
        GameObject gridCell = gridMaker.gridCells[cell.x, cell.y];
        UpdateVisual(gridCell, cell);
        gridCell.GetComponent<NodeVisual>().SetColorBlue();
    }
    public IEnumerator SetCellToGreen(Cell cell)
    {
        yield return new WaitForSecondsRealtime(delayStep);

        GridMaker gridMaker = pathFinder.gridMaker;
        GameObject gridCell = gridMaker.gridCells[cell.x, cell.y];
        UpdateVisual(gridCell, cell);
        gridCell.GetComponent<NodeVisual>().SetColorGreen();
    }
   
}
