
using UnityEngine;
using TMPro;

public class Count : MonoBehaviour
{
    public TextMeshProUGUI counterText;  // 拖入你的 TextMeshPro 对象
    private int count = 0;

    // 在 UnityEvent 中调用这个
    public void AddOne()
    {
        
        count++;
        counterText.text = "Count: " + count;
    }
}