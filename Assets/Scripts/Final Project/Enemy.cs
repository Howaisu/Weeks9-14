using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is attached to the enemy object
    It is using only for the moving and rotating of the enemies
    I used a trick, because the camera is the child of the player, and the center of it is always in the center, so I use that position
 
 */
public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotateSpeed = 360f; // The angle that rotate in second

    private Camera mainCam; //reference of the camera
   
    void Start()
    {
        mainCam = Camera.main; //assign the data
    }

   
    void Update()
    {
        MoveEnemyToPlayer();
    }
    void MoveEnemyToPlayer()
    {
        if (mainCam == null) return;

        // looking for the center position as the world loction
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 worldTarget = mainCam.ScreenToWorldPoint(new Vector3(screenCenter.x, screenCenter.y));
        worldTarget.z = 0f; // Keep the z axis 0 to stay in 2D

        // Move the enemy towards to the player
        Vector3 direction = (worldTarget - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotating the enemy heading to the player(similar as the player)
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}
