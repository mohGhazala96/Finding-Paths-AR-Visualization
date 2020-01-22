using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SearchMethod
{
    BFS,
    DFS,
    Dijkstra,
    AStar
}
public class PathFinding : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public GridMaker gridMaker;
    private Grid grid;
    private List<Cell> openList;
    private List<Cell> closedList;
    public SearchMethod searchMethod= SearchMethod.BFS;
    public SearchVisualizer searchVisualizer;
    List<Cell> finalResultPath;
    public List<Vector3> walkakblePath;
    public IEnumerator GetWalkablePath(int endX, int endY)
    {
        grid = gridMaker.grid;
        switch (searchMethod)
        {
            case SearchMethod.BFS:
               yield return StartCoroutine( BFS(endX, endY));
                break;
            case SearchMethod.DFS:
                yield return StartCoroutine(DFS(endX, endY));
                break;
            case SearchMethod.Dijkstra:
                yield return StartCoroutine(Dijkstra(endX, endY));
                break;
            case SearchMethod.AStar:
                yield return StartCoroutine(AStarSearch(endX, endY));
                break;

        }

        if (finalResultPath == null)
        {
             yield return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (Cell cell in finalResultPath)
            {
                vectorPath.Add(gridMaker.gridCells[cell.x, cell.y].gameObject.transform.position);
            }
            walkakblePath= vectorPath;
        }
    }

    IEnumerator BFS(int endX, int endY)
    {
        Cell startNode = grid.cells[0, 0];
        Cell endNode = grid.cells[endX, endY];
        Queue<Cell> nodesToExamine = new Queue<Cell>();
        HashSet<Cell> exploredNodes= new HashSet<Cell>();
        nodesToExamine.Enqueue(startNode);
        exploredNodes.Add(startNode);
        while (nodesToExamine.Count > 0)
        {
            Cell currentNode = nodesToExamine.Dequeue();
            yield return StartCoroutine( searchVisualizer.SetCellToGreen(currentNode));

            if (currentNode == endNode)
            {
                gridMaker.gameState = GameState.Finished;
                yield return CalculatePath(endNode, startNode);
                yield break;

            }

            List<Cell> NeighbouringCells = GetNeighbourList(currentNode);
            foreach (Cell cell in NeighbouringCells)
            {
                if (!cell.isObstacle&&!exploredNodes.Contains(cell))
                {
                    yield return  StartCoroutine(searchVisualizer.SetCellToBlue(cell));
                    exploredNodes.Add(cell);
                    cell.cameFromNode = currentNode;
                    nodesToExamine.Enqueue(cell);
                }
            }
            yield return  StartCoroutine(searchVisualizer.SetCellToRed(currentNode));
        }
        gridMaker.gameState = GameState.Finished;
        yield break;

    }

    IEnumerator DFS(int endX, int endY)
    {
        Cell startNode = grid.cells[0, 0];
        Cell endNode = grid.cells[endX, endY];
        Stack<Cell> nodesToExamine = new Stack<Cell>();
        HashSet<Cell> exploredNodes = new HashSet<Cell>();
        nodesToExamine.Push(startNode);
        exploredNodes.Add(startNode);

        while (nodesToExamine.Count > 0)
        {
            Cell currentNode = nodesToExamine.Pop();
            yield return StartCoroutine(searchVisualizer.SetCellToGreen(currentNode));
            if (currentNode == endNode)
            {
                gridMaker.gameState = GameState.Finished;
                yield return CalculatePath(endNode, startNode);
                yield break;

            }

            List<Cell> NeighbouringCells = GetNeighbourList(currentNode);
            foreach (Cell cell in NeighbouringCells)
            {
                if (!cell.isObstacle && !exploredNodes.Contains(cell))
                {
                    yield return StartCoroutine(searchVisualizer.SetCellToBlue(cell));
                    exploredNodes.Add(cell);
                    cell.cameFromNode = currentNode;
                    nodesToExamine.Push(cell);
                }
            }
            yield return StartCoroutine(searchVisualizer.SetCellToRed(currentNode));

        }
        gridMaker.gameState = GameState.Finished;
        yield break;

    }

    IEnumerator Dijkstra(int endX, int endY)
    {


        Cell startNode = grid.cells[0, 0];
        Cell endNode = grid.cells[endX, endY];
        HashSet<Cell> nodesToExamine = new HashSet<Cell>();
        IDictionary<Cell, int> distances = new Dictionary<Cell, int>();
        distances.Add(startNode, 0);
        startNode.weight = 0;

        foreach (Cell cell in gridMaker.grid.cells)
        {
            if (!cell.isObstacle && cell!=startNode)
            {
                distances.Add(cell, int.MaxValue);
                nodesToExamine.Add(cell);
                cell.weight = int.MaxValue;
            }
        }

        Cell currentNode = startNode;
        yield return StartCoroutine(searchVisualizer.SetCellToGreen(currentNode));

        while (nodesToExamine.Count > 0)
        {

            if (currentNode == endNode)
            {
                gridMaker.gameState = GameState.Finished;
                yield return CalculatePath(endNode, startNode);
                yield break;
            }
            nodesToExamine.Remove(currentNode);
            List<Cell> nodes = GetNeighbourList(currentNode);

            foreach (Cell node in nodes)
            {
                if (!node.isObstacle)
                {
                    int dist = distances[currentNode] + CalculateDistanceCost(currentNode, node);
                    node.weight = dist;
                    if (dist < distances[node])
                    {
                        distances[node] = dist;
                        node.cameFromNode = currentNode;
                        yield return StartCoroutine(searchVisualizer.SetCellToBlue(node));
                    }
                }

               
            }
            yield return StartCoroutine(searchVisualizer.SetCellToRed(currentNode));
            currentNode = distances.Where(x => nodesToExamine.Contains(x.Key)).OrderBy(x => x.Value).First().Key;
            yield return StartCoroutine(searchVisualizer.SetCellToGreen(currentNode));

        }
        gridMaker.gameState = GameState.Finished;
        yield break;

    }


    IEnumerator AStarSearch(int endX, int endY)
    {
        Cell startNode = grid.cells[0,0];
        Cell endNode = grid.cells[endX, endY];

       
        openList = new List<Cell> { startNode };
        closedList = new List<Cell>();

        for (int x = 0; x < gridMaker.rows; x++)
        {
            for (int y = 0; y < gridMaker.coloumns; y++)
            {
                Cell pathNode = grid.cells[x, y];
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            Cell currentNode = GetLowestFCostNode(openList);
            yield return StartCoroutine(searchVisualizer.SetCellToGreen(currentNode));

            if (currentNode == endNode)
            {
                gridMaker.gameState = GameState.Finished;
                yield return CalculatePath(endNode,startNode);
                yield break;
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Cell neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (neighbourNode.isObstacle)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                        yield return StartCoroutine(searchVisualizer.SetCellToRed(neighbourNode));
                    }
                }
                yield return StartCoroutine(searchVisualizer.SetCellToRed(currentNode));

            }
        }

        gridMaker.gameState = GameState.Finished;
        yield break;
    }

    private List<Cell> GetNeighbourList(Cell currentNode) {
        List<Cell> neighbourList = new List<Cell>();

        if (currentNode.x - 1 >= 0) {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // Left Up
            if (currentNode.y + 1 < gridMaker.rows) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < gridMaker.rows) {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // Right Up
            if (currentNode.y + 1 < gridMaker.rows) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < gridMaker.rows) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    public Cell GetNode(int x, int y) {
        return grid.cells[x, y];
    }

    IEnumerator CalculatePath(Cell endNode, Cell startNode) {
        List<Cell> path = new List<Cell>();
        Cell currentNode = endNode;
        while (currentNode != startNode) {
            yield return StartCoroutine(searchVisualizer.SetCellToGreen(currentNode));
            path.Add(currentNode);
            currentNode = currentNode.cameFromNode;
        }
        yield return StartCoroutine(searchVisualizer.SetCellToGreen(startNode));
        path.Reverse();
        finalResultPath= path;

    }

    private int CalculateDistanceCost(Cell a, Cell b) {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private Cell GetLowestFCostNode(List<Cell> pathNodeList) {
        Cell lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++) {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost) {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
