using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 This script is attaching to the GAME CONTROLLER

It is for control the movement of the player's ship, including: move, rotation, limitation of move, and speed up
 
 
 */

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerShip; 
    public GameObject secondLayer;
    public Vector3 mousePos;

    //
    public Vector3 pointing; // where the player pointing at
    //

    public float moveSpeed = 5f; // The speed of player (it kinda like static here, because it'a multiplier, speed actually change with the distance of mouse)
    //--------------For listener----------------//
    public Spawner spawner;

    public GameObject TopLeft;
    public GameObject BottomRight;
    //BOOSTER
    public float boostedSpeed = 15f; // The speed boosts to
    public float boostTime = 5f; // The duration of boost the speed
    private Coroutine speedBoostCoroutine;
    public Slider boostSlider; // The Slider for counting down the boost_time
    public GameObject speedText; //An animation shows when boosting





    void Start()
    {
        if (spawner != null && spawner.SpeedUp != null)
        {
            spawner.SpeedUp.AddListener(speedup); //A listener to invoke the boosting, listen from spawner-eating
        }

    }
        void Update()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //I tried to just write this once, but it's turns out use own calculation is better
            Vector3 direction = mousePos - transform.position; //same as this one
            spinMaker();
            moveMaker(direction);
            ClampPlayerPosition();

        //shooting
        //
        LockPlayerZPosition();

        }

        void spinMaker()
        {
        // Accurately obtain mouse coordinates and operate only on the XY plane to avoid Z value interference** very similar to the last assignment
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 direction = mousePosition - secondLayer.transform.position;

            // Avoid the direction is 0
            if (direction.sqrMagnitude > 0.001f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //
                angle -= 90; //But Idk what cause it always turn extra 90, so I fix that by hard code

                // Turn the object[not for the whole thing, becuase I don't wanna turn the camera]
                secondLayer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            pointing = direction;
        }
        void moveMaker(Vector3 mouse) //for this time I used mouse=direction that wrote in Update
    {
       
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0; //lock the z (2D)




            // The object only moves when the cursor little bit far away£¨avoiding shaking£©
            if (mouse.magnitude > 0.1f)
            {
                // get the direction from ship to mouse
                Vector3 Direction = (mousePos - playerShip.transform.position).normalized;

                // Move the ship in the direction of the mouse
                playerShip.transform.position += Direction * moveSpeed * Time.deltaTime;

                //   Debug.Log(Direction);
            }
        }

    void speedup()
    {
        //moveSpeed = moveSpeed + 0.2f; //It's called not once, but would loop more than one time(like a second), so it would get a number around 2-3
        //Debug.Log("---Listener Activated: collision with speed up prefab---");

        
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine); //
        }
        // The coroutine get reset each time the coroutine starts
        speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine());

    }
    //THis is the coroutine for the boost
    IEnumerator SpeedBoostCoroutine()
    {
        moveSpeed = boostedSpeed; // Change the speed to the speed that boosting to
        Debug.Log("---Listener Activated: collision with speed up prefab---");

        // 
        if (boostSlider != null)
        {
            boostSlider.maxValue = boostTime;
            boostSlider.value = boostTime;
            boostSlider.gameObject.SetActive(true); // Show the Slider
        }
        if (speedText != null)
        {   
            speedText.SetActive(true);
        }

        float timer = boostTime; //IT is how many second it have
        while (timer > 0) //while loop, can be use in a coroutine. The loop 
        {
            timer -= Time.deltaTime; //make the timer->slider-value-fill goes down smoothly> each frame is something like -0.01s
            //and the timer will be reset when next time call this coroutine because the line ahead
            if (boostSlider != null)
            {
                boostSlider.value = timer; // Update the Slider
            }
            yield return null; //wait for next frame
        }

        moveSpeed = 5f; // Go back to the normal speed
        if (boostSlider != null)
        {
            boostSlider.gameObject.SetActive(false); 
        }
        if (speedText != null)
        {
            speedText.SetActive(false);
        }
        speedBoostCoroutine = null; // reset the reference of the coroutine
    }


    //always have problem witih the z value so I lock this ship's z to 0
    void LockPlayerZPosition()
    {
        // Ensure the player's Z position is always 0, it runs away from the screen before
       
            Vector3 position = playerShip.transform.position;
            playerShip.transform.position = new Vector3(position.x, position.y, 0);
        
    }
    void ClampPlayerPosition()
    {
        //Again, using the clamp method to restric the player's location. I think it should have some better mothods
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