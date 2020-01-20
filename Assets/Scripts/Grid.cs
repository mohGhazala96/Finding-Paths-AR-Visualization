using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    public Cell[,] cells;

    public Grid(int rows,int coloumns)
    {
        cells = new Cell[rows,coloumns];
        // fill cell with its weights
        for (int i = 0; i < rows; i++){
            for (int j = 0; j < coloumns; j++){
                cells[i, j] = new Cell(i,j, false);
            }
        }

    }
    
}
