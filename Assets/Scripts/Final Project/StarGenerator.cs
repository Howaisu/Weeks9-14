using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject starPrefab_A; // 星星的Prefab
    public GameObject starPrefab_B;

    public GameObject topLeft;    // 左上角的定位物体
    public GameObject downBottom; // 右下角的定位物体

    public int star1Count = 200;   // 星星数量
    public int star2Count = 150;

    void Start()
    {
        GenerateStarsType1();
        GenerateStarsType2();
    }

    void GenerateStarsType1()
    {
        Vector3 topLeftPos = topLeft.transform.position;
        Vector3 downBottomPos = downBottom.transform.position;

        float minX = Mathf.Min(topLeftPos.x, downBottomPos.x);
        float maxX = Mathf.Max(topLeftPos.x, downBottomPos.x);
        float minY = Mathf.Min(topLeftPos.y, downBottomPos.y);
        float maxY = Mathf.Max(topLeftPos.y, downBottomPos.y);

        for (int i = 0; i < star1Count; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            Vector3 spawnPos = new Vector3(randomX, randomY, 0);

            Instantiate(starPrefab_A, spawnPos, Quaternion.identity);
        }
    }

    void GenerateStarsType2()
    {
        Vector3 topLeftPos = topLeft.transform.position;
        Vector3 downBottomPos = downBottom.transform.position;

        float minX = Mathf.Min(topLeftPos.x, downBottomPos.x);
        float maxX = Mathf.Max(topLeftPos.x, downBottomPos.x);
        float minY = Mathf.Min(topLeftPos.y, downBottomPos.y);
        float maxY = Mathf.Max(topLeftPos.y, downBottomPos.y);

        for (int i = 0; i < star2Count; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            Vector3 spawnPos = new Vector3(randomX, randomY, 0);

            Instantiate(starPrefab_B, spawnPos, Quaternion.identity);
        }
    }
}
