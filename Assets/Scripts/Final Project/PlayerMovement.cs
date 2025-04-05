using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject secondLayer;
    public Vector3 mousePos;

    public float moveSpeed = 5f; // 玩家移动速度，可在Inspector中调整

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        spinMaker();
        moveMaker(direction);
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
}