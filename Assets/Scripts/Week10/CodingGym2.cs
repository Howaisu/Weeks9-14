using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class CodingGym2 : MonoBehaviour
{
    public float speed = 1f;
    public GameObject car;
    public float theX;
    public float target = 7;

    void Start()
    {
       
        // StartCoroutine(Move());
    }

    void Update()
    {
        //if (theX < 7)
        //{
        //    theX = Mathf.Lerp(0,7, speed * Time.deltaTime);
        //    Debug.Log(theX);
            
        //}
    }

    public void GO()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        //Vector3 startPos = car.transform.position;
        //Vector3 targetPos = new Vector3(car.transform.position.x+theX, startPos.y, 0);

        //while (Vector3.Distance(startPos,targetPos)>0.2f)
        //{
        //    transform.position = Vector3.Lerp(startPos,targetPos ,speed*Time.deltaTime);
        //    yield return null;
        //}\
     
       

        while (theX < target)
        {
            Vector3 startPos = car.transform.position;
            theX = Mathf.Lerp(theX,theX+target,speed);
            Debug.Log(theX);
            Vector3 targetPos = new Vector3(car.transform.position.x + theX, startPos.y, 0);
            transform.position = targetPos;
            yield return null;
        }

   
    }
}
