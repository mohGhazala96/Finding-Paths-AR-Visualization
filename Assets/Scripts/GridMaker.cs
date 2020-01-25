using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Solving,
    Finished,
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
    private Vector3 origin;
    public float sepration;
    private List<GameObject> allCells;
    public GameObject flag;
    public GameObject allGameobjects;
    public GameState gameState;
    public GameObject player;
    public Camera arCamera;

    private void Start()
    {
        allCells = new List<GameObject>();
        allGameobjects.transform.localPosition =Vector3.zero;
        origin = allGameobjects.transform.position;

        Init();
    }
    public void Init()
    {
        gameState = GameState.Clear;
        gridCells = new GameObject[rows, coloumns];
        if (allCells.Capacity > 0)
        {
            foreach(GameObject cell in allCells)
            {
                cell.transform.parent = null;
                Destroy(cell);
            }
        }
        allCells = new List<GameObject>();

        // choose from menu
        grid = new Grid(rows,coloumns);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < coloumns; j++)
            {
                Vector3 position = new Vector3(origin.x+(j*sepration), origin.y, origin.z+(i*sepration));
                grid.cells[i, j].isObstacle = false;
                gridCells[i, j]=Instantiate(walkableCell, Vector3.zero, allGameobjects.transform.rotation, allGameobjects.transform);
                gridCells[i, j].transform.localPosition = position;
                gridCells[i, j].name = grid.cells[i, j].ToString();
                allCells.Add(gridCells[i, j]);
            }
        }
        Vector3 firstCellPosition = gridCells[0,0].transform.localPosition;
        player.transform.localPosition = new Vector3(firstCellPosition.x, 1, firstCellPosition.z);
        flag.transform.localPosition = gridCells[rows - 1, rows - 1].transform.localPosition + (0.5f * gridCells[rows - 1, rows - 1].transform.localScale);
    }

    public void GenerateRandomObstacles()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < coloumns; j++)
            {
                if (!(i==0 && j==0)&&!(j==coloumns-1 && i==rows-1))
                {
                    if (Random.Range(0, 4) == 2)
                    {
                        GenerateObstacle(i, j);

                    }
                }
                

            }
        }
    }
    void GenerateObstacle(int x,int y)
    {
        if (!grid.cells[x, y].isObstacle)
        {
            grid.cells[x, y].isObstacle = true;
            int heightDrop = 2;
            Vector3 position = new Vector3(gridCells[x, y].transform.localPosition.x, gridCells[x, y].transform.localPosition.y + heightDrop, gridCells[x, y].transform.localPosition.z);
            GameObject obstacle = Instantiate(obstacleCell, Vector3.zero, allGameobjects.transform.rotation, allGameobjects.transform);
            obstacle.transform.localPosition = position;

            float cellSize = gridCells[x, y].transform.localScale.y;
            StartCoroutine(obstacle.GetComponent<Obstacle>().Drop(obstacle.transform.localPosition, gridCells[x, y].transform.localPosition.y + cellSize));
            allCells.Add(obstacle);
            obstacle.name = gridCells[x, y].name;
            gridCells[x, y].GetComponent<MeshRenderer>().enabled = false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
    
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && gameState.Equals(GameState.Clear)) // && haven't started editing
        {

            var ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
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
                        GenerateObstacle(xPos, yPos);
                    }
                    else
                    {
                        grid.cells[xPos, yPos].isObstacle = false;
                        Destroy(selection.gameObject);
                        gridCells[xPos, yPos].GetComponent<MeshRenderer>().enabled = true;
                    }

                }
            
            }
        }
    }
}
