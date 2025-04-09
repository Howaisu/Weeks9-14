using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public GameObject starPrefab_A;
    public GameObject starPrefab_B;

    public GameObject topLeft;
    public GameObject downBottom;

    public int star1Count = 200;
    public int star2Count = 150;

    public GameObject enemy;
    public int howmanyEnemy = 60;
    public List<GameObject> targetEnemy;

    public GameObject player;  // Reference to the player GameObject

    public float npcSpeed = 2f;
    //Items
    //1 MOVEMENT SPEED
    public int howmanyItemsA = 10;
    public float floatingSpeed = 1f;
    public GameObject speedingItem;
    public List<GameObject> targetItemA;

    //2 SHOOT SPEED
    public int howmanyItemsB = 15;
    public GameObject shootingItem;
    public List<GameObject> targetItemB;
    //
    public UnityEvent SpeedUp = new UnityEvent();
    public UnityEvent LaserFast = new UnityEvent();
    //
   
    void Start()
    {
        GenerateStarsType1();
        GenerateStarsType2();
        spawnController();
        itemGenerator();
    }

    void Update()
    {
        // MoveEnemiesTowardsPlayer();
        CheckCollisions();
    }

    void GenerateStarsType1()
    {
        SpawnStars(starPrefab_A, star1Count);
    }

    void GenerateStarsType2()
    {
        SpawnStars(starPrefab_B, star2Count);
    }

    void SpawnStars(GameObject prefab, int count)
    {
        Vector3 topLeftPos = topLeft.transform.position;
        Vector3 downBottomPos = downBottom.transform.position;

        float minX = Mathf.Min(topLeftPos.x, downBottomPos.x);
        float maxX = Mathf.Max(topLeftPos.x, downBottomPos.x);
        float minY = Mathf.Min(topLeftPos.y, downBottomPos.y);
        float maxY = Mathf.Max(topLeftPos.y, downBottomPos.y);

        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            Vector3 spawnPos = new Vector3(randomX, randomY, 0);

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

    void itemGenerator()
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

    void CheckCollisions()
    {
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
                //Destroy(theItem);
                theItem.SetActive(false);
            }
        }
    }


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