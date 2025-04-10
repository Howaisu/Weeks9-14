
using UnityEngine;
using TMPro;

/*
    This code is attaching to the UI object
    Now is just for show the enemy count and some text like speed
    I want to make the kill-streak in this code it there's time
 
    I think this code is too easy to explain, the only high-light is the listener component, but it is explained in another code
 */



//This kind of event method is out of scope, I think
//public static class GameEvents 
//{
//    public static System.Action OnEnemyHit;
//}

public class Count : MonoBehaviour
{
    //Counter
    public TextMeshProUGUI counterText;  
    //private int count = 0;
    public int count;
    public TextMeshProUGUI Speed;
    public TextMeshProUGUI shootingFrequency;
    //public Bullet kill; // reference Bullet component;; alright, this also not work because there are too many clones
    public Spawner spawner;
    public PlayerMovement playerMovement;
    public void Start()
    {
       
            if (spawner.HitEnemy != null)
            {
                spawner.HitEnemy.AddListener(IncreaseCount);
                Debug.Log("Event listener added successfully.");// I must be fool, I used an hour to know I forgot assign the spawner until use the debug
            }
          
    }
    public void Update()
    {
        counterText.text = "Count: " + count;
        Speed.text = "Speed: " + playerMovement.moveSpeed * 100 + " km/h"; 
        //shootingFrequency.text
    }


    //void OnEnable()
    //{
    //    GameEvents.OnEnemyHit += IncreaseCount;
    //}

    //void OnDisable()
    //{
    //    GameEvents.OnEnemyHit -= IncreaseCount;
    //}

    public void IncreaseCount()
    {
        count++;
        
    }
}