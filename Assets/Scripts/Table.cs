using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private bool isAvailable = true;
    [SerializeField] private int numSeats;
    private readonly List<Seat> _seats = new List<Seat>();

    private Renderer renderer;
    private Restaurant restaurant;

    void Start()
    {
        restaurant = Restaurant.Instance;
        
        renderer = transform.GetChild(0).GetComponent<Renderer>();

        var allSeats = transform.parent.GetComponentsInChildren<Seat>();
        _seats.AddRange(allSeats);

        numSeats = _seats.Count;
        
        restaurant.RegisterTable(this);
    }

    public int GetSeatCount()
    {
        return numSeats;
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

    public List<Seat> GetSeats()
    {
        return _seats;
    }

    public void SetAvailability(bool newAvailability)
    {
        
        isAvailable = newAvailability;
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }
}
