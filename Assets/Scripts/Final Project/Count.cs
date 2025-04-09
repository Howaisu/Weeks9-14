
using UnityEngine;
using TMPro;

public class Count : MonoBehaviour
{
    public TextMeshProUGUI counterText;  // ������� TextMeshPro ����
    private int count = 0;

    // �� UnityEvent �е������
    public void AddOne()
    {
        
        count++;
        counterText.text = "Count: " + count;
    }
}