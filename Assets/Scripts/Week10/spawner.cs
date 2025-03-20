using UnityEngine;

public class SpawnPrefabInScreenSpace : MonoBehaviour
{
    public GameObject prefabToSpawn; 
    public Canvas canvas; //
    public AnimationCurve size;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            SpawnPrefabAtMousePosition();
        }
    }

    void SpawnPrefabAtMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition; 
        mousePosition.z = 10f; 

        
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

       
        Instantiate(prefabToSpawn, worldPosition, Quaternion.identity);
    }
}
