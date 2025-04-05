using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject starPrefab_A;
    public GameObject starPrefab_B;

    public GameObject topLeft;
    public GameObject downBottom;

    public int star1Count = 200;
    public int star2Count = 150;

    public GameObject enemy;
    [SerializeField] int howmanyEnemy = 60;
    public List<GameObject> targetEnemy;

    public GameObject player;  // Reference to the player GameObject

    public float npcSpeed = 2f;

    void Start()
    {
        GenerateStarsType1();
        GenerateStarsType2();
        spawnController();
    }

    void Update()
    {
        MoveEnemiesTowardsPlayer();
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

    void MoveEnemiesTowardsPlayer()
    {
        if (player == null) return;

        foreach (GameObject enemyObj in targetEnemy)
        {
            if (enemyObj != null)
            {
                Vector3 direction = (player.transform.position - enemyObj.transform.position).normalized;
                enemyObj.transform.position += direction * npcSpeed * Time.deltaTime;
            }
        }
    }
}
