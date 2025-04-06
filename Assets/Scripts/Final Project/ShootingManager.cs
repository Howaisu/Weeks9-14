using System.Collections;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public GameObject thingPrefab;
    public Bullet currentThing;

    //
    public GameObject dummyShot;

    public float spawnFrequency = 0.2f;

    // 类级变量，记住上一次shoot状态
   // private bool shootSecondBullet = false;

    void Start()
    {
        StartCoroutine(GenerateRoutine());
    }

    void Update()
    {
        Shoots();
    }

    IEnumerator GenerateRoutine()
    {
        while (true)
        {
          
            yield return new WaitForSeconds(spawnFrequency);

            if (currentThing == null)
            {
             
                GameObject real = Instantiate(thingPrefab, transform.position, transform.rotation);
                real.transform.parent = transform;

                currentThing = real.GetComponent<Bullet>();
                Debug.Log("Bullet Loaded");
            }
        }
    }


    void Shoots()
    {
        // 1. 按下鼠标，发射真实子弹
        if (Input.GetMouseButtonDown(0) && currentThing != null)
        {
            currentThing.Fire();
            currentThing = null;
          
           
            Debug.Log(" Shoot Bullet");
        }

       
       
    }



}
