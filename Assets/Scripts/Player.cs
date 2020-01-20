using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PathFinding pathFinder;
    IEnumerator MovePlayer(List<Vector3>  positions)
    {
        foreach (Vector3 postion in positions)
        {
            gameObject.transform.position = new Vector3(postion.x, gameObject.transform.position.y, postion.z);
            yield return new WaitForSeconds(1);
        }

  
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            List<Vector3> positions = pathFinder.GetWalkablePath(4, 4);

            //StartCoroutine(MovePlayer(positions));
        }
    }
}
