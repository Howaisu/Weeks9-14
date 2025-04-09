using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotateSpeed = 360f; // 每秒旋转角度（可调）

    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemyToPlayer();
    }
    void MoveEnemyToPlayer()
    {
        if (mainCam == null) return;

        // 1?? 获取屏幕中心对应的世界坐标
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 worldTarget = mainCam.ScreenToWorldPoint(new Vector3(screenCenter.x, screenCenter.y, mainCam.nearClipPlane + 5f));
        worldTarget.z = 0f; // 保持 2D 平面上

        // 2?? 移动物体朝向目标点
        Vector3 direction = (worldTarget - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 3?? 旋转物体朝向目标方向
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}
