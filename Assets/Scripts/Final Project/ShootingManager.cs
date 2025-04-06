using System.Collections;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public GameObject thingPrefab;
    public Bullet currentThing;

    //
    public GameObject dummyShot;

    public float spawnFrequency = 0.2f;

    // �༶��������ס��һ��shoot״̬
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
        // 1. ������꣬������ʵ�ӵ�
        if (Input.GetMouseButtonDown(0) && currentThing != null)
        {
            currentThing.Fire();
            currentThing = null;
          
           
            Debug.Log(" Shoot Bullet");
        }

       
       
    }



}
