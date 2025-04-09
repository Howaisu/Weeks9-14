
using UnityEngine;
using TMPro;
public static class GameEvents
{
    public static System.Action OnEnemyHit;
}

public class Count : MonoBehaviour
{
    public TextMeshProUGUI counterText;  // 拖入你的 TextMeshPro 对象
    private int count = 0;
    public TextMeshProUGUI Speed;
    public TextMeshProUGUI shootingFrequency;
    //public Bullet kill; // reference Bullet component;; alright, this also not work because there are too many clones

    public void Start()
    {
        counterText.text = "Count: " + count;
        //  kill.HitEnemy.AddListener(AddOne);
    }
   

    void OnEnable()
    {
        GameEvents.OnEnemyHit += IncreaseCount;
    }

    void OnDisable()
    {
        GameEvents.OnEnemyHit -= IncreaseCount;
    }

    void IncreaseCount()
    {
        count++;
        counterText.text = "Count: " + count;
    }
}