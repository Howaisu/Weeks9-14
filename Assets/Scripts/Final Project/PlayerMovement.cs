using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject secondLayer;
    public Vector3 mousePos;

    //
    public Vector3 pointing; // where the player pointing at

    public float moveSpeed = 5f; // ����ƶ��ٶȣ�����Inspector�е���

    //
    public GameObject bulletPrefab; // �ӵ�Prefab
    public Transform firePoint; // �ӵ����ɵĳ�ʼλ�ã���Ϊ����Ӷ���

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        spinMaker();
        moveMaker(direction);

        //shooting
        // ����������ӵ�
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    void spinMaker()
    {
        // ��ȷ��ȡ������꣬��ֻ��XYƽ���ϲ���������Zֵ����
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = mousePosition - secondLayer.transform.position;

        // ������������Ч������Ϊ��������
        if (direction.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // �˴����� Quaternion.AngleAxis ��ȷ��ת����
            secondLayer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        pointing = direction;
    }
    void moveMaker(Vector3 mouse)
    {

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0; //lock the z (2D)

       

      
        // ����Ҿ������λ�ý�Զʱ���ƶ������ⶶ����
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

        // �����ӵ���Fire���������ӵ���ǰ�˶�
        bullet.GetComponent<ShootingManager>().Fire();
    }
}