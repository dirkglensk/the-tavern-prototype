using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    private Renderer renderer;
    private bool isAvailable = true;
    // Start is called before the first frame update
    void Start()
    {
        renderer = transform.GetChild(0).GetComponentInChildren<Renderer>();
    }

    void Update()
    {
        if (isAvailable)
        {
            renderer.material.color = Color.green;
        }
        else
        {
            renderer.material.color = Color.red;
        }
    }

    public Transform GetSeatPosition()
    {
        return transform;
    }

    public void SetAvailability(bool newState)
    {
        isAvailable = newState;
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }
}
