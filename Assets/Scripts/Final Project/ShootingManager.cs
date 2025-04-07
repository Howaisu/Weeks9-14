using System.Collections;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public GameObject thingPrefab;            // �ӵ� prefab
    public Bullet currentThing;               // ��ǰ����װ����ӵ�
    public float spawnFrequency = 0.2f;       // ���Ƶ�ʣ����ʱ�䣩
    public GameObject spawnerObject;          // Spawner GameObject�����ڴ��� Bullet��

    private Coroutine generateCoroutine;      // ΨһЭ������

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
        // �����ظ���Э�̣�Э���Զ���� currentThing
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
            currentThing = null; // ����������ã�����
        }
    }
}
