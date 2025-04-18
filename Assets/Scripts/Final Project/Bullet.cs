using UnityEngine;
using UnityEngine.Events;
/*
 This code might not look very long, but it is the hardest code in my project!!
 Because this code is attached on the Prefab of laser, so all the data is difficult to transfer
 Over 8 hours spent on this code, not gonna lie.
 
 */

public class Bullet : MonoBehaviour
{
    public bool isFired = false;
    public float speed = 20;

    public GameObject enemySpawner;
    private Spawner spawner; // Refeence to Spawner 
                             // Thank to Bullet bulletScript = laser.GetComponent<Bullet>();when generating it. It can reference another code YEAH
   
      // I tried to invoke events, but none of them work
     //public UnityEvent HitEnemy;  
    // public UnityEvent HitEnemy = new UnityEvent();
    //
    //public GameObject UI;
    //private Count counter; //

    private void Start()
    {
        spawner = enemySpawner.GetComponent<Spawner>();
       

    }

    void Update()
    {
        if (isFired)
        {
            transform.SetParent(null);//This line took me *5 Hours* to get��
            /*\
             There was a bug is that the bullet will always be the transform of the parent object at the last moment,
             and would not be detached until the next prefab is generated.I knew it was because the parent problem, but I put them in the Fire() it not work
             it turns out that the sequence matter when I randomly put this line here.
             */
            transform.position += transform.up * speed * Time.deltaTime;

            CheckHitEnemies(); // Check it hitting the enemies
        }
        else
        {
            //**These code doesn't nessary now, because the bullet has already rotating by following the parent GameObject beofre fired.
            //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 direction = mousePos - (Vector2)transform.position;
            //transform.up = direction;
        }
    }
    //The fire method call by Shoots() in ShootingManger,a code learned from in in-class activity
    public void Fire()
    {
        isFired = true;

    
        if (transform.parent != null)    // Okay so this avoid a warning error in Unity
        {
            transform.SetParent(null); // jump out from the parent
        }

        Debug.Log("Shoot out");

        Destroy(gameObject, 5); //I was using coroutine, but I found it was not better than this
    }


    public void CheckHitEnemies()
    {
        
        //There was a HUGE problem with the hitting enemies. I thought it would be as easy as my fish project, but it was not!
        /*
         The prefab can't assign a GameObject, and I don't know it at beginning, I used debug to check which area have problem for so many times
         Because the prefab create and disappear in a short time, so I did not see what happening in it
         But I finlly find the problem, and it is super difficult to solve
         */
        // Debug.Log("processing the Hitting Enemies");
        if (spawner == null || spawner.targetEnemy == null) return; //I originally don't know what cause the problem, and I tried many methods to stop 

        for (int i = spawner.targetEnemy.Count - 1; i >= 0; i--)
        {

            GameObject enemy = spawner.targetEnemy[i];

            //if (enemy == null) continue;

            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < 0.5f)
            {
                Destroy(enemy);
                spawner.targetEnemy.RemoveAt(i);
                spawner.killEnemy();//OKAY? It is simple like that??? I spend hours and hours try to find out how this communicate, but the Invoke still failed
                Debug.Log("BOOM! Enemy destroyed!");
                //counter.count ++;
                // HitEnemy.Invoke();//This is the first try
                //GameEvents.OnEnemyHit?.Invoke();//The try of the pro event communication, but it is out of scope

                //doesn't work at first, because it can't assign anything
                //I also tried to make it invoke the listener, but listener also need to reference to one, there are too many clones

                //   Destroy(gameObject); //I think it is still the communication issue, the shooting Manager can't get the 'null' information
                // I tried more than 3 hours to make this work, but at the end I failed

                break;
            }
        }


    }
}

// Sooo tired :(