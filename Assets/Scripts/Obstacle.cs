using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public IEnumerator Drop(Vector3 pos,float posY)
    {

        float i = 0.0f;
        int time = Random.Range(1, 2);
        float rate = 1.0f / time;
        Vector3 startPos = pos;
        Vector3 endPos = new Vector3(pos.x, posY, pos.z);

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localPosition = Vector3.Lerp(startPos, endPos, i);
            yield return null;

        }



    }
    // Update is called once per frame
    void Update()
    {

    }
}
