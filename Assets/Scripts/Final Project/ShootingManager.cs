using System.Collections;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public float speed = 10f;
    private bool isFired = false;

    void Update()
    {
        if (isFired)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    public void Fire()
    {
        isFired = true;
        transform.parent = null;

        // 启动协程销毁子弹
        StartCoroutine(DestroyAfterTime(10f));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}