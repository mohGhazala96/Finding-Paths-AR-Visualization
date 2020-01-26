using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PathFinding pathFinder;
    public Animator animator;

    IEnumerator AnimatePlayer()
    {
        yield return StartCoroutine(pathFinder.GetWalkablePath(pathFinder.gridMaker.rows - 1, pathFinder.gridMaker.rows - 1));
        if(pathFinder.walkakblePath.Count > 0)
        {
            foreach (Vector3 postion in pathFinder.walkakblePath)
            {
                yield return new WaitForSeconds(1f);
                gameObject.transform.LookAt(postion);
                gameObject.transform.localEulerAngles = new Vector3(0, gameObject.transform.localEulerAngles.y, 0);
                animator.Play("Jump");
                yield return new WaitForSeconds(0.75f);
                float time = 1f;
                Vector3 startPos = gameObject.transform.position;
                Vector3 endPos = new Vector3(postion.x, gameObject.transform.position.y, postion.z);
                yield return StartCoroutine(MovePlayer(startPos,endPos,time));
            }
        }
        else 
        {
            UIManager.Instance.errorText.SetActive(true);
        }

        yield return null;
    }

    public IEnumerator MovePlayer(Vector3 startPos, Vector3 endPos,float time)
    {

        float rate = 1.5f;
        float timer = 0;
        while (timer <= time)
        {
            timer += Time.deltaTime * rate;
            gameObject.transform.position = Vector3.Lerp(startPos, endPos, timer);
            yield return null;

        }
  
    }

    public void Move()
    {
        StartCoroutine(AnimatePlayer());
    }
}
