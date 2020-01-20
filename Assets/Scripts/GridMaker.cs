﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Solving,
    Clear
}
public class GridMaker : MonoBehaviour
{
    public Grid grid;
    public GameObject[,] gridCells;
    public GameObject walkableCell;
    public GameObject obstacleCell;
    public int rows;
    public int coloumns;
    public Vector3 origin;
    public int sepration;
    private List<GameObject> allCells;
    public GameState gameState;
    private void Start()
    {
        allCells = new List<GameObject>();
        Init();
    }
    public void Init()
    {
        gameState = GameState.Clear;
        origin = Vector3.zero;
        gridCells= new GameObject[rows, coloumns];
        if (allCells.Capacity > 0)
        {
            foreach(GameObject cell in allCells)
            {
                Destroy(cell);
            }
            allCells = new List<GameObject>();
        }
        // choose from menu
        grid = new Grid(rows,coloumns);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < coloumns; j++)
            {
                Vector3 position = new Vector3(origin.x+(j*sepration), origin.y, origin.z+(i*sepration));
                grid.cells[i, j].isObstacle = false;
                gridCells[i, j]=Instantiate(walkableCell, position, Quaternion.identity);
                gridCells[i, j].name = grid.cells[i, j].ToString();
                allCells.Add(gridCells[i, j]);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& gameState.Equals(GameState.Clear)) // && haven't started editing
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag("Cell"))
                {
                    string[] splitArray = selection.name.Split(char.Parse(","));
                    int xPos = int.Parse(splitArray[0]);
                    int yPos = int.Parse(splitArray[1]);
                    if (!grid.cells[xPos, yPos].isObstacle)
                    {
                        grid.cells[xPos, yPos].isObstacle = true;
                        Vector3 position = gridCells[xPos, yPos].transform.position;
                        GameObject obstacle = Instantiate(obstacleCell, position, Quaternion.identity);
                        allCells.Add(obstacle);
                        obstacle.name = gridCells[xPos, yPos].name;
                    }
                    else
                    {
                        grid.cells[xPos, yPos].isObstacle = false;
                        Destroy(selection.gameObject);
                    }

                }
            
            }
        }
    }
}