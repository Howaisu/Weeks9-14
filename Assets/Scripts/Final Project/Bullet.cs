using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static event System.Action OnBulletDestroyed;

    public bool isFired = false;
    public float speed = 20f;

    public GameObject enemySpawner;
    private Spawner spawner;

    public ShootingManager shootingManager;

    void Start()
    {
        // ֻ��ʵ������ GetComponent
        if (enemySpawner != null && enemySpawner.scene.IsValid())
        {
            spawner = enemySpawner.GetComponent<Spawner>();
        }
    }

    void Update()
    {
        if (isFired)
        {
            if (transform.parent != null)
                transform.SetParent(null);

            transform.position += transform.up * speed * Time.deltaTime;
            CheckHitEnemies();
        }
    }

    public void Fire()
    {
        if (!gameObject.scene.IsValid())
        {
            Debug.LogWarning("?? Attempted to fire a prefab, not a scene instance!");
            return;
        }

        isFired = true;

        if (transform.parent != null)
            transform.SetParent(null);

        Destroy(gameObject, 5f); // ��ʱ�Ի�
    }

    public void CheckHitEnemies()
    {
        if (spawner == null || spawner.targetEnemy == null) return;

        for (int i = spawner.targetEnemy.Count - 1; i >= 0; i--)
        {
            GameObject enemy = spawner.targetEnemy[i];
            if (enemy == null) continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < 0.5f)
            {
                Destroy(enemy);
                spawner.targetEnemy.RemoveAt(i);

                Debug.Log("?? BOOM! Enemy destroyed!");

                Destroy(gameObject); // �Զ����� OnDestroy()
                break;
            }
        }
    }

    void OnDestroy()
    {
        // ȷ������ prefab ��Դ
        if (!gameObject.scene.IsValid()) return;

        OnBulletDestroyed?.Invoke(); // �¼��㲥
        Debug.Log("?? Bullet destroyed and event fired");
    }
}
