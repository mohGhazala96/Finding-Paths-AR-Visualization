using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            Vector3 position = new Vector3(gridCells[x, y].transform.position.x, gridCells[x, y].transform.position.y + Random.Range(2, 5), gridCells[x, y].transform.position.z);
            GameObject obstacle = Instantiate(obstacleCell, position, Quaternion.identity);
            StartCoroutine(obstacle.GetComponent<Obstacle>().Drop(obstacle.transform.position, gridCells[x, y].transform.position.y));
            allCells.Add(obstacle);
            obstacle.name = gridCells[x, y].name;
            gridCells[x, y].GetComponent<MeshRenderer>().enabled = false;
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
                        GenerateObstacle(xPos, yPos);
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
