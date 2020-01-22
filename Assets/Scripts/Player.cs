using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PathFinding pathFinder;
    public Animator animator;
    IEnumerator MovePlayer()
    {
        yield return StartCoroutine(pathFinder.GetWalkablePath(pathFinder.gridMaker.rows - 1, pathFinder.gridMaker.rows - 1));
        if (pathFinder.walkakblePath != null)
        {
            foreach (Vector3 postion in pathFinder.walkakblePath)
            {
                animator.Play("Jump");
                float time = 1.1f;
                Vector3 startPos = gameObject.transform.position;
                Vector3 endPos = new Vector3(postion.x, gameObject.transform.position.y, postion.z);
                yield return StartCoroutine(TransitionPlayer(startPos,endPos,time));
            }

        }
        else
        {
            UIManager.Instance.errorText.SetActive(true);
        }
        yield return null;
    }
    public IEnumerator TransitionPlayer(Vector3 startPos, Vector3 endPos,float time)
    {

        float rate = 1.0f / time;
        float timer = 0;
        while (timer < time)
        {
            timer += Time.deltaTime * rate;
            gameObject.transform.position = Vector3.Lerp(startPos, endPos, timer);
            yield return null;

        }


    }
    public void Move()
    {
        StartCoroutine(MovePlayer());
    }
}
