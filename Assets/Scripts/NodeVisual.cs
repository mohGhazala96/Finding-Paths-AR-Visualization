using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeVisual : MonoBehaviour
{
    public GameObject weightText;
    public GameObject hCostText;
    public GameObject fCostText;
    public GameObject gCostText;

    public void UpdateWeight(Cell cell)
    {
        int weight = cell.weight;
        weightText.SetActive(true);
        hCostText.SetActive(false);
        fCostText.SetActive(false);
        gCostText.SetActive(false);
    }

    public void UpdateCosts(Cell cell)
    {
        int hCost = cell.hCost;
        int fCost = cell.fCost;
        int gCost = cell.gCost;
        weightText.SetActive(false);
        hCostText.SetActive(true);
        fCostText.SetActive(true);
        gCostText.SetActive(true);
    }

    public void SetColorBlue()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
    public void SetColorGreen()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
    public void SetColorRed()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
