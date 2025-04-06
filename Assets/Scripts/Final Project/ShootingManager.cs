using System.Collections;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public GameObject thingPrefab;
    public Bullet currentThing;

    //public GameObject dummyShot;

    public float spawnFrequency = 0.2f;

    public GameObject spawnerObject; // ? ���� Spawner GameObject���� Spawner �ű��ģ�

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

                // ? �� spawnerObject ���� Bullet �ű���
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


