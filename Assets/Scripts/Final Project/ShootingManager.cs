using System.Collections;
using UnityEngine;
/*
 This code is attached on the Player Object
 The purpose is make the player shoot the "Laser Bullet" out in the correct TYPE.
 The general functions are close to the CNC card
 
 */
public class ShootingManager : MonoBehaviour
{
    // public GameObject thingPrefab; This is for the first experiment,It is based on the in-class script
    //ALL THE LASER BULLET TYPES'PREFAB:
    public GameObject laserTypeOne;
    public GameObject laserTypeTwo;
    public GameObject laserTypeThree;
    //The Laser Type: 
    public int currentLaserType; //saving the current switch by int, if there are more, I think I can use 'case'


    //Reference of the Bullet Class
    public Bullet currentThing;

    //public GameObject dummyShot; I tried use some strange way to solve a bug, but it was fixed after hustle

    public float spawnFrequency = 0.2f; //This is the reloading speed of the prefab, which means, the longer it is, the slower the shooting frequence would be

    public GameObject spawnerObject; // Those are for reference the spawner script
    public Spawner spawner;

    void Start()
    {
        StartCoroutine(GenerateRoutine()); //RUN the loop ALL TIME in game
        if (spawner != null && spawner.LaserFast != null)
        {
            spawner.LaserFast.AddListener(ShootSpeedUp); //invoke by eating the item
        }
    }

    void Update()
    {
        Shoots();
    }

    IEnumerator GenerateRoutine() //reloading
    {
        //This code is kind like a reference from the in-class tutorial, but change the 'space button' reloading to an auto-reloading controlled by a coroutine
        while (true)
        {
            yield return new WaitForSeconds(spawnFrequency); //so this is how much 'time' to reload

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
                //Those are different prefab to Instatiate by the difference of laser_type which triggers by the "event trigger" 


                //  make spawnerObject go into Bullet script
                Bullet bulletScript = laser.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.enemySpawner = spawnerObject;
                }//this is very very important, I looked for this method for countless hours. I don't remember we learn FindByTypes, and without this,
                //I think we have to use another event system (commented in the Count.cs, and also written in the previous planning, that is over scope,too)

                laser.transform.parent = transform;
                currentThing = bulletScript;
            }
        }
    }
    //Shooting function, very similar to the code learned in class
    void Shoots()
    {
        if (Input.GetMouseButtonDown(0) && currentThing != null)
        {
            currentThing.Fire();
            currentThing = null;
        }
    }
    //Item: Shooting Speed Up
    public void ShootSpeedUp()
    {
        Debug.Log("Commucated EVENT: shooting speed++");
        spawnFrequency = spawnFrequency / 2; //getting smaller and smaller but never to 0
    }
    //Switch Buttons, call by event trigger
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