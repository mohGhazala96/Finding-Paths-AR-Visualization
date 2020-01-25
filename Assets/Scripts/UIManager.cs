using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GridMaker gridMaker;
    public Player player;
    public GameObject solveButton;
    public GameObject clearButton;
    public GameObject errorText;
    public GameObject legend;
    private static UIManager instance;
    public Text legendText; 
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    public void Solve()
    {
        if (!gridMaker.gameState.Equals(GameState.Clear))
            return;

        player.Move();
        gridMaker.gameState = GameState.Solving;
        solveButton.SetActive(false);
        clearButton.SetActive(true);

    }

    public void Clear()
    {
        gridMaker.gameState = GameState.Clear;
        player.StopAllCoroutines();
        gridMaker.StopAllCoroutines();
        player.pathFinder.StopAllCoroutines();
        gridMaker.Init();
        solveButton.SetActive(true);
        clearButton.SetActive(false);
        errorText.SetActive(false);
    }
    public void ChangeTimePerStep(int index)
    {
        if (gridMaker.gameState.Equals(GameState.Solving) || gridMaker.gameState.Equals(GameState.Finished))
            return;

        switch (index)
        {
            case 0:
                player.pathFinder.searchVisualizer.delayStep = 0.5f;
                break;
            case 1:
                player.pathFinder.searchVisualizer.delayStep = 0.5f/2;
                break;
            case 2:
                player.pathFinder.searchVisualizer.delayStep = 0.5f / 4;
                break;
            case 3:
                player.pathFinder.searchVisualizer.delayStep = 0.5f / 8;
                break;
        }
    }

    public void ChangeSize(int index)
    {
        if (gridMaker.gameState.Equals(GameState.Solving) || gridMaker.gameState.Equals(GameState.Finished))
            return;

        switch (index)
        {
            case 0:
                player.pathFinder.gridMaker.allGameobjects.transform.localScale=Vector3.one;
                break;
            case 1:
                player.pathFinder.gridMaker.allGameobjects.transform.localScale = Vector3.one*2;
                break;
            case 2:
                player.pathFinder.gridMaker.allGameobjects.transform.localScale = Vector3.one*3;
                break;
            case 3:
                player.pathFinder.gridMaker.allGameobjects.transform.localScale = Vector3.one*4;
                break;
        }
    }

    public void ChangeSearch(int index)
    {
        if (gridMaker.gameState.Equals(GameState.Solving) || gridMaker.gameState.Equals(GameState.Finished))
            return;

        switch (index)
        {
            case 0:
                player.pathFinder.searchMethod = SearchMethod.BFS;
                break;
            case 1:
                player.pathFinder.searchMethod = SearchMethod.DFS;
                break;
            case 2:
                player.pathFinder.searchMethod = SearchMethod.Dijkstra;
                break;
            case 3:
                player.pathFinder.searchMethod = SearchMethod.AStar;
                break;
        }
    }

    public void GenerateRandomObstacles()
    {
        if (gridMaker.gameState.Equals(GameState.Solving) || gridMaker.gameState.Equals(GameState.Finished))
            return;
        gridMaker.GenerateRandomObstacles();
    }

    public void ChangeGrid(int index)
    {
        if (gridMaker.gameState.Equals(GameState.Solving) || gridMaker.gameState.Equals(GameState.Finished))
            return;
        switch (index)
        {
            case 0:
                gridMaker.rows = 4;
                gridMaker.coloumns = 4;

                break;
            case 1:
                gridMaker.rows = 8;
                gridMaker.coloumns = 8;
                break;
            case 2:
                gridMaker.rows = 16;
                gridMaker.coloumns = 16;
                break;
            case 3:
                gridMaker.rows = 32;
                gridMaker.coloumns = 32;
                break;
        }
        gridMaker.Init();
    }
    public void ToggleLegend()
    {
        legend.SetActive(!legend.activeInHierarchy);
        if (!legend.activeInHierarchy)
        {
            legendText.text = "Show Legend";
        }
        else
        {
            legendText.text = "Hide Legend";
        }
    }
    
}
