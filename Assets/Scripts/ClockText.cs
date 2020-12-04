using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockText : MonoBehaviour
{
    private Restaurant restaurant;

    private TextMeshProUGUI TextMesh;

    private const int RealHoursPerDay = 24;
    private const int RealMinutesPerHour = 60;
    
    // Start is called before the first frame update
    void Start()
    {
        restaurant = Restaurant.Instance;
        TextMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var gameTime = Mathf.Lerp(0, RealMinutesPerHour * RealHoursPerDay, restaurant.GetCurrentTime() / restaurant.GetDayLength());
        gameTime = gameTime / RealMinutesPerHour;
        var percentOfGameHour = (float)(gameTime - Math.Truncate(gameTime));
        var gameSeconds = Mathf.Lerp(0, 60, percentOfGameHour);
        TextMesh.text = $"{gameTime:00}:{gameSeconds:00}\nDay {restaurant.GetCurrentDay()}";
    }
}
