using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeVisual : MonoBehaviour
{
    public Text weightText;
    public Text hCostText;
    public Text fCostText;
    public Text gCostText;
    public GameObject nodeVisual;
    public void UpdateWeight(Cell cell)
    {
        weightText.text = cell.weight.ToString();
        weightText.gameObject.SetActive(true);
        hCostText.gameObject.SetActive(false);
        fCostText.gameObject.SetActive(false);
        gCostText.gameObject.SetActive(false);
    }

    public void UpdateCosts(Cell cell)
    {
        hCostText.text = cell.hCost.ToString();
        fCostText.text = cell.fCost.ToString();
        gCostText.text = cell.gCost.ToString();

        weightText.gameObject.SetActive(false);
        hCostText.gameObject.SetActive(true);
        gCostText.gameObject.SetActive(true);
        fCostText.gameObject.SetActive(true);
    }
    public void ActivateVisual()
    {
        nodeVisual.SetActive(true);
    }
    public void SetColorBlue()
    {
        ActivateVisual();
        nodeVisual.GetComponent<Renderer>().material.color = Color.blue;
    }
    public void SetColorGreen()
    {
        ActivateVisual();
        nodeVisual.GetComponent<Renderer>().material.color = Color.green;
    }
    public void SetColorRed()
    {
        ActivateVisual();
        nodeVisual.GetComponent<Renderer>().material.color = Color.red;
    }
}   
