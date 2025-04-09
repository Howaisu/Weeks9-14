using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject secondLayer;
    public Vector3 mousePos;

    //
    public Vector3 pointing; // where the player pointing at
    //

    public float moveSpeed = 5f; // 玩家移动速度，可在Inspector中调整
    //For listener
    public Spawner spawner;

    public GameObject TopLeft;
    public GameObject BottomRight;





    void Start()
    {
        if (spawner != null && spawner.SpeedUp != null)
        {
            spawner.SpeedUp.AddListener(speedup);
        }

    }
        void Update()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - transform.position;
            spinMaker();
            moveMaker(direction);
            ClampPlayerPosition();

        //shooting
        //
        LockPlayerZPosition();

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
                //
                angle -= 90;

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

                //   Debug.Log(Direction);
            }
        }

    void speedup()
    {
        moveSpeed = moveSpeed + 0.2f; //It's called not once, but would loop more than one time(like a second), so it would get a number around 2-3
        Debug.Log("---Listener Activated: collision with speed up prefab---");

    }
    //always have problem witih the z value so I lock this ship's z to 0
    void LockPlayerZPosition()
    {
        // Ensure the player's Z position is always 0
        if (playerShip.transform.position.z != 0)
        {
            Vector3 position = playerShip.transform.position;
            playerShip.transform.position = new Vector3(position.x, position.y, 0);
        }
    }
    void ClampPlayerPosition()
    {
        Vector3 position = playerShip.transform.position;
        float minX = Mathf.Min(TopLeft.transform.position.x, BottomRight.transform.position.x);
        float maxX = Mathf.Max(TopLeft.transform.position.x, BottomRight.transform.position.x);
        float minY = Mathf.Min(TopLeft.transform.position.y, BottomRight.transform.position.y);
        float maxY = Mathf.Max(TopLeft.transform.position.y, BottomRight.transform.position.y);

        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        playerShip.transform.position = position;
    }





    //

}