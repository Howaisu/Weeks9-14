using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isFired = false;
    public float speed = 5;

    void Update()
    {
        if (isFired)
        {
            transform.SetParent(null);
            transform.position += transform.up * speed * Time.deltaTime;
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

        // ÏÈ³¹µ×ÍÑÀë¸¸¼¶
        transform.SetParent(null);
        transform.SetParent(null);
        Debug.Log("Shoot out");

        Destroy(gameObject, 5);
    }

    public void HitEnemy()
    { 
    
    
    }
}
