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
        // 只有实例才能 GetComponent
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

        Destroy(gameObject, 5f); // 超时自毁
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

                Destroy(gameObject); // 自动触发 OnDestroy()
                break;
            }
        }
    }

    void OnDestroy()
    {
        // 确保不是 prefab 资源
        if (!gameObject.scene.IsValid()) return;

        OnBulletDestroyed?.Invoke(); // 事件广播
        Debug.Log("?? Bullet destroyed and event fired");
    }
}
