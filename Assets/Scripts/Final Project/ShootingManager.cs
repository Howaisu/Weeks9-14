using System.Collections;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    // public GameObject thingPrefab; This is for the first experiment,It is based on the in-class script
    //ALL THE LASER BULLET TYPES'PREFAB:
    public GameObject laserTypeOne;
    public GameObject laserTypeTwo;
    public GameObject laserTypeThree;
    //The Laser Type: 
    public int currentLaserType;


    //Reference of the Bullet Class
    public Bullet currentThing;

    //public GameObject dummyShot;

    public float spawnFrequency = 0.2f;

    public GameObject spawnerObject; //  拖入 Spawner GameObject（带 Spawner 脚本的）

    void Start()
    {
        StartCoroutine(GenerateRoutine()); //RUN the loop ALL TIME in game
    }

    void Update()
    {
        Shoots();
    }

    IEnumerator GenerateRoutine() //reloading
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnFrequency);

            if (currentThing == null)
            {
                GameObject laser;
                if (currentLaserType == 1)
                {
                    laser = Instantiate(laserTypeOne, transform.position, transform.rotation);
                }
                else if (currentLaserType == 2)
                {
                     laser = Instantiate(laserTypeTwo, transform.position, transform.rotation);
                }
                else if (currentLaserType == 3)
                {
                     laser = Instantiate(laserTypeThree, transform.position, transform.rotation);
                }
                else
                {
                     laser = Instantiate(laserTypeOne, transform.position, transform.rotation);
                }



                //  把 spawnerObject 传入 Bullet 脚本中
                Bullet bulletScript = laser.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.enemySpawner = spawnerObject;
                }

                laser.transform.parent = transform;
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
    //Item: Shooting Speed Up
    void ShootSpeedUp()
    {

        spawnFrequency = spawnFrequency / 2;
    }
    //Switch Buttons
    public void ChangeToLaserOne()
    {
        currentLaserType = 1;
        Debug.Log(currentLaserType);
    
    }
    public void ChangeToLaserTwo()
    {
        currentLaserType = 2;
        Debug.Log(currentLaserType);

    }
    public void ChangeToLaserThree()
    {
        currentLaserType = 3;
        Debug.Log(currentLaserType);

    }
}