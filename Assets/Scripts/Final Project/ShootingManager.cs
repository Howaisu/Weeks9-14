using System.Collections;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    // public GameObject thingPrefab; This is for the first experiment,It is based on the in-class script

    //ALL THE LASER BULLET TYPES'PREFAB
    public GameObject laserTypeOne;
    public GameObject laserTypeTwo;
    public GameObject laserTypeThree;


    //


    //Reference of the Bullet Class
    public Bullet currentThing;

    //public GameObject dummyShot;

    public float spawnFrequency = 0.2f;

    public GameObject spawnerObject; //  拖入 Spawner GameObject（带 Spawner 脚本的）

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
                GameObject real = Instantiate(laserTypeOne, transform.position, transform.rotation);

                // ? 把 spawnerObject 传入 Bullet 脚本中
                Bullet bulletScript = real.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.enemySpawner = spawnerObject;
                }

                real.transform.parent = transform;
                currentThing = bulletScript;
            }
        }
    }

    void Shoots()
    {
        if (Input.GetMouseButtonDown(0) && currentThing != null)
        {
            currentThing.Fire();
            currentThing = null;
        }
    }
}