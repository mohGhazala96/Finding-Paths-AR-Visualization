using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GridMaker gridMaker;
    public Player player;
    public GameObject solveButton;
    public GameObject clearButton;

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
    }

    public void DropDownIndexChange(int index)
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
