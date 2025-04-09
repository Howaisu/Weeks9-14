
using UnityEngine;
using TMPro;

public class Count : MonoBehaviour
{
    public TextMeshProUGUI counterText;  // 拖入你的 TextMeshPro 对象
    private int count = 0;
    public TextMeshProUGUI Speed;
    public TextMeshProUGUI shootingFrequency;
    //public Bullet kill; // reference Bullet component;; alright, this also not work because there are too many clones


    public void Start()
    {
      //  kill.HitEnemy.AddListener(AddOne);
    }
    // 在 UnityEvent 中调用这个
    public void AddOne()
    {
        Debug.Log("+1");
        count++;
        counterText.text = "Count: " + count;
    }
}