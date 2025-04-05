using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject secondLayer;
    public Vector3 mousePos;

    //
    public Vector3 pointing; // where the player pointing at

    public float moveSpeed = 5f; // 玩家移动速度，可在Inspector中调整

    //
    public GameObject bulletPrefab; // 子弹Prefab
    public Transform firePoint; // 子弹生成的初始位置（作为玩家子对象）

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        spinMaker();
        moveMaker(direction);

        //shooting
        // 鼠标点击发射子弹
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    void spinMaker()
    {
        // 精确获取鼠标坐标，且只在XY平面上操作，避免Z值干扰
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - secondLayer.transform.position;

        // 若方向向量有效（避免为零向量）
        if (direction.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 此处采用 Quaternion.AngleAxis 精确旋转对象
            secondLayer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        pointing = direction;
    }
    void moveMaker(Vector3 mouse)
    {

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0; //lock the z (2D)

       

      
        // 当玩家距离鼠标位置较远时才移动（避免抖动）
        if (mouse.magnitude > 0.1f)
        {
            // get the direction from fish to mouse
            Vector3 Direction = (mousePos - playerShip.transform.position).normalized;

            // Move playerFish in the direction of the mouse
            playerShip.transform.position += Direction * moveSpeed * Time.deltaTime;
          
            Debug.Log(Direction);
        }
    }

    //
    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, firePoint);

        // 调用子弹的Fire方法，让子弹向前运动
        bullet.GetComponent<ShootingManager>().Fire();
    }
}