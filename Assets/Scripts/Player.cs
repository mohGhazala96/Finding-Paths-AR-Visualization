using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PathFinding pathFinder;
    IEnumerator MovePlayer(List<Vector3>  positions)
    {
        if (positions!=null)
        {
            foreach (Vector3 postion in positions)
            {
                gameObject.transform.position = new Vector3(postion.x, gameObject.transform.position.y, postion.z);
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            UIManager.Instance.errorText.SetActive(true);
        }
        
        yield return null;
    }

    public void Move()
    {
        List<Vector3> positions = pathFinder.GetWalkablePath(pathFinder.gridMaker.rows-1, pathFinder.gridMaker.rows - 1); 
        StartCoroutine(MovePlayer(positions));
    }
}
