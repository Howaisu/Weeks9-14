using System.Collections;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public GameObject thingPrefab;            // 子弹 prefab
    public Bullet currentThing;               // 当前正在装填的子弹
    public float spawnFrequency = 0.2f;       // 射击频率（间隔时间）
    public GameObject spawnerObject;          // Spawner GameObject（用于传给 Bullet）

    private Coroutine generateCoroutine;      // 唯一协程引用

    void Start()
    {
        generateCoroutine = StartCoroutine(GenerateRoutine());
    }

    void Update()
    {
        Shoots();
    }

    void OnEnable()
    {
        Bullet.OnBulletDestroyed += HandleBulletDestroyed;
    }

    void OnDisable()
    {
        Bullet.OnBulletDestroyed -= HandleBulletDestroyed;
    }

    void HandleBulletDestroyed()
    {
        currentThing = null;
        // 不再重复启协程，协程自动检测 currentThing
    }

    IEnumerator GenerateRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnFrequency);

            if (currentThing == null)
            {
                GameObject real = Instantiate(thingPrefab, transform.position, transform.rotation);

                Bullet bulletScript = real.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.enemySpawner = spawnerObject;
                    bulletScript.shootingManager = this;
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
            currentThing = null; // 主动清除引用，保险
        }
    }
}
