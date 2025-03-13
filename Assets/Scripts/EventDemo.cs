using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 using UnityEngine.EventSystems;
//using textMeshPro

public class EventDemo : MonoBehaviour
{
    public RectTransform banana;
    // Start is called before the first frame update
    // public TMPro.TextMeshPro mPro;

    public UnityEvent OnTimeHasFinish;
    public float timerLength = 3;
    public float t;

    public void MouseJustEnteredImage()
    {
        Debug.Log("Hello?");
        banana.localScale = Vector3.one * 1.2f;
    }

    public void MouseJustExitImage()
    {
        Debug.Log("Bye!");
        banana.localScale = Vector3.one;

    }
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > timerLength)
        { 
            OnTimeHasFinish.Invoke();
            t = timerLength;
        }
       
    }
}
