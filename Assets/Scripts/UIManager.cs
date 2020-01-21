using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GridMaker gridMaker;
    public Player player;
    public GameObject solveButton;
    public GameObject clearButton;
    public GameObject errorText;
    private static UIManager instance;

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

    // Start is called before the first frame update
    public void Solve()
    {
        player.Move();
        gridMaker.gameState = GameState.Solving;
        solveButton.SetActive(false);
        clearButton.SetActive(true);

    }

    public void Clear()
    {
        gridMaker.gameState = GameState.Clear;
        gridMaker.Init();
        player.gameObject.transform.position = new Vector3(gridMaker.origin.x, player.gameObject.transform.position.y, gridMaker.origin.z);
        solveButton.SetActive(true);
        clearButton.SetActive(false);
        errorText.SetActive(false);
        // dont clear obstacles such that you can view them
    }
    public void ChangeSearch(int index)
    {
        if (gridMaker.gameState.Equals(GameState.Solving))
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
        gridMaker.GenerateRandomObstacles();
    }
    public void ChangeGrid(int index)
    {
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
