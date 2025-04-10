using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/*
    This code is attached to Game Controller
    It is for generating the game assets on map, including: stars, enemis, and items
    
    It also includes the function of eating(collision), because the data of each array can be using directly

 
 */
/// </summary>
public class Spawner : MonoBehaviour
{
    //two kinds of stars
    public GameObject starPrefab_A;
    public GameObject starPrefab_B;
    //the generation area [limitor]
    public GameObject topLeft;
    public GameObject downBottom;
    //how many stars
    public int star1Count = 200;
    public int star2Count = 150;
    //ENEMY
    public GameObject enemy;
    public int howmanyEnemy = 60;
    public List<GameObject> targetEnemy;
    //PLAYER
    public GameObject player;  // Reference to the player GameObject, using for collision

    public float npcSpeed = 2f;
    //-----------Items
    //1 MOVEMENT SPEED
    public int howmanyItemsA = 10;
    public float floatingSpeed = 1f;
    public GameObject speedingItem;
    public List<GameObject> targetItemA;

    //2 SHOOT SPEED
    public int howmanyItemsB = 15;
    public GameObject shootingItem;
    public List<GameObject> targetItemB;
    //------------
    ////EVENT 
    public UnityEvent SpeedUp = new UnityEvent();
    public UnityEvent LaserFast = new UnityEvent();
     public UnityEvent HitEnemy = new UnityEvent();


    void Start()
    {
        SpawnStars(starPrefab_A, star1Count);   //Generate stars
        SpawnStars(starPrefab_B, star2Count);
        spawnController();
        //itemGenerator(speedingItem,howmanyItemsA);
        //itemGenerator(shootingItem, howmanyItemsB);
        itemGeneratorA();   //Generate the items
        itemGeneratorB();
    }

    void Update()
    {
        // MoveEnemiesTowardsPlayer();
        CheckCollisions();
        CheckCollisionB();
        

    }


    //Those are the limitor for spawn items. 
    //Each time through the loop, a random x and y coordinate is chosen within the bounds.
    //These random coordinates are combined to form a spawn position(spawnPos).
    void SpawnStars(GameObject prefab, int count)
    {
        //These are two objects I put inside the map, to get the edge's position
        Vector3 topLeftPos = topLeft.transform.position;
        Vector3 downBottomPos = downBottom.transform.position;
        //get the Interval first
        float minX = Mathf.Min(topLeftPos.x, downBottomPos.x);
        float maxX = Mathf.Max(topLeftPos.x, downBottomPos.x);
        float minY = Mathf.Min(topLeftPos.y, downBottomPos.y);
        float maxY = Mathf.Max(topLeftPos.y, downBottomPos.y);
        //EACH TIME, get a random number between the limits, and then generate the object based on the prefab.
        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(minX, maxX); 
            float randomY = Random.Range(minY, maxY);

            Vector3 spawnPos = new Vector3(randomX, randomY, 0);
            //combines the positions to a Vector3
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

    void spawnController()
    {
        targetEnemy = new List<GameObject>();

        Vector3 topLeftPos = topLeft.transform.position;
        Vector3 downBottomPos = downBottom.transform.position;

        float minX = Mathf.Min(topLeftPos.x, downBottomPos.x);
        float maxX = Mathf.Max(topLeftPos.x, downBottomPos.x);
        float minY = Mathf.Min(topLeftPos.y, downBottomPos.y);
        float maxY = Mathf.Max(topLeftPos.y, downBottomPos.y);

        for (int i = 0; i < howmanyEnemy; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            GameObject newTarget = Instantiate(enemy);
            newTarget.transform.position = new Vector3(randomX, randomY, 0);

            targetEnemy.Add(newTarget);
        }
    }

    //Item Generate

    void itemGeneratorA()
    {
        targetItemA = new List<GameObject>();
      

        Vector3 topLeftPos = topLeft.transform.position;
        Vector3 downBottomPos = downBottom.transform.position;

        float minX = Mathf.Min(topLeftPos.x, downBottomPos.x);
        float maxX = Mathf.Max(topLeftPos.x, downBottomPos.x);
        float minY = Mathf.Min(topLeftPos.y, downBottomPos.y);
        float maxY = Mathf.Max(topLeftPos.y, downBottomPos.y);

        for (int i = 0; i < howmanyItemsA; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            GameObject newTarget = Instantiate(speedingItem);
            newTarget.transform.position = new Vector3(randomX, randomY, 0);

            targetItemA.Add(newTarget);
        }
    }
    void itemGeneratorB()
    {
        targetItemB = new List<GameObject>();


        Vector3 topLeftPos = topLeft.transform.position;
        Vector3 downBottomPos = downBottom.transform.position;
        //I think this function can be write once, but Imma keeping like this for now
        float minX = Mathf.Min(topLeftPos.x, downBottomPos.x);
        float maxX = Mathf.Max(topLeftPos.x, downBottomPos.x);
        float minY = Mathf.Min(topLeftPos.y, downBottomPos.y);
        float maxY = Mathf.Max(topLeftPos.y, downBottomPos.y);

        for (int i = 0; i < howmanyItemsB; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            GameObject newTarget = Instantiate(shootingItem);
            newTarget.transform.position = new Vector3(randomX, randomY, 0);

            targetItemB.Add(newTarget);
        }
    }
    //This code is similar to the code I used in last assignment, BUT!! I used more than half an hour to fix the problem, the problem is
    //the player is not in the same z value with the target objects! Change to Vector can't help anything.fixed by LOCK Z to 0
    void CheckCollisions()
    {
        //ITEM A
        for (int i = targetItemA.Count - 1; i >= 0; i--) // Loop from last to first
        {
           // Debug.Log(i);
            GameObject theItem = targetItemA[i];
            Vector3 Pposition = new Vector3(player.transform.position.x, player.transform.position.y,0) ;

       //     Debug.Log("Item position: " + item.transform.position);
         //   Debug.Log("Player position: " + Pposition);

            float distance = Vector3.Distance(Pposition, theItem.transform.position);
         //   Debug.Log("Distance to item: " + distance);

            if (distance < 0.5f) // Collision threshold
            {
              // Debug.Log("Eat item");
                SpeedUp.Invoke();
                //Destroy(theItem); //Idk why it destroy the prefab
                theItem.SetActive(false);
            }
        }

    }
    //I tried to write the collision together, but it have errors, so I am keep like this
    void CheckCollisionB()
    {


        //ITEM B
        for (int i = targetItemB.Count - 1; i >= 0; i--) // Loop from last to first
        {
            // Debug.Log(i);
            GameObject items = targetItemB[i];
            // Vector3 Pposition = new Vector3(player.transform.position.x, player.transform.position.y, 0);

            //     Debug.Log("Item position: " + item.transform.position);
            //   Debug.Log("Player position: " + Pposition);

            float distance = Vector3.Distance(player.transform.position, items.transform.position);// I've already make player 0 in the other script
            //   Debug.Log("Distance to item: " + distance);

            if (distance < 0.5f) // Collision threshold
            {
                // Debug.Log("Eat item");
                LaserFast.Invoke();
                //Destroy(theItem);
                items.SetActive(false);
            }
        }


    }
    //Invoke the event of killing enemy from here, I want to do the kill-streak inside Count if have time
    public void killEnemy()
    {
        Debug.Log("Trigger the Event");
        HitEnemy.Invoke();
    
    }

    /////Moved this code to Enemy.cs
    //void MoveEnemiesTowardsPlayer()
    //{
    //    if (player == null) return;

    //    foreach (GameObject enemyObj in targetEnemy)
    //    {
    //        if (enemyObj != null)
    //        {
    //            Vector3 direction = (player.transform.position - enemyObj.transform.position).normalized;
    //            enemyObj.transform.position += direction * npcSpeed * Time.deltaTime;
    //        }
    //    }
    //}
}