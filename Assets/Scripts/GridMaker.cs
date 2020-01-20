using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        origin = Vector3.zero;
        gridCells= new GameObject[rows, coloumns];

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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // && haven't started editing
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
