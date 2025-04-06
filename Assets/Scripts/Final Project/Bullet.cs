using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isFired = false;
    public float speed = 5;

    private Spawner spawner; // ����Spawner�ű�
    void Update()
    {
        if (isFired)
        {
            transform.SetParent(null);
            transform.position += transform.up * speed * Time.deltaTime;

            //CheckHitEnemies(); // ÿ֡�����ײ
        }
        else
        {
            //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 direction = mousePos - (Vector2)transform.position;
            //transform.up = direction;
        }
    }

    public void Fire()
    {
        isFired = true;

        // �ȳ������븸��
        if (transform.parent != null)
        {
            transform.SetParent(null); // ? ֻ����һ�Σ�ȷ��������ʱʵ��
        }

        Debug.Log("Shoot out");

        Destroy(gameObject, 5);
    }


    //public void CheckHitEnemies()
    //{
    //    Debug.Log("processing the Hitting Enemies");
    //    if (spawner == null || spawner.targetEnemy == null) return;

    //    for (int i = spawner.targetEnemy.Count - 1; i >= 0; i--)
    //    {
    //        GameObject enemy = spawner.targetEnemy[i];

    //        if (enemy == null) continue;

    //        float distance = Vector3.Distance(transform.position, enemy.transform.position);

    //        if (distance < 0.5f)
    //        {
    //            Destroy(enemy);
    //            spawner.targetEnemy.RemoveAt(i);

    //            Debug.Log("?? Enemy destroyed!");

    //            // ����������ӵ�Ҳ��ʧ
    //            Destroy(gameObject);
    //            break;
    //        }
    //    }


    //}
}


