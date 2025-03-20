using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodingGym : MonoBehaviour
{
    public bool switcher;
    public AnimationCurve brightness;
    public float t;
    public Vector2 startLocation;
    public Renderer theLight;

    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position; // Save the initial position
        
    }

    // Update is called once per frame
    void Update()
    {
        if (switcher) // Continuously update alpha only if switcher is true
        {
            t += Time.deltaTime; // Increment t by the time passed since last frame
            UpdateAlpha(); // Update the alpha value based on the animation curve
        }
    }

    void UpdateAlpha()
    {
        float alpha = brightness.Evaluate(t); // Evaluate the curve at time t
        Debug.Log("t ="+t+"brightness" + brightness.Evaluate(t));
        Color newColor = theLight.material.color;
        newColor.a = alpha; // Set the alpha to the evaluated value
        GetComponent<Renderer>().material.color = newColor; // Apply the new color with updated alpha
    }

    public void TurnOn()
    {
        switcher = true;
        Debug.Log("It is " + switcher);
        // t is incremented in Update to smoothly transition based on time
    }

    public void TurnOff()
    {
        switcher = false;
        Debug.Log("It is " + switcher);
        t = 0f; // Reset time
        UpdateAlpha(); // Immediately apply the initial alpha state
    }
}
