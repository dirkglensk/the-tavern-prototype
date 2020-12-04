using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Reception : MonoBehaviour
{    
    // Init
    private readonly List<PatronGroup> waitingPatronGroups = new List<PatronGroup>();
    private bool isReadyForNextGroup = false;
    private bool isOperated = false;
    private float debugTimer = 0f;

    // References
    private Restaurant _restaurant;
    
    void Start()
    {
        _restaurant = Restaurant.Instance;
        
        //Debug
        isOperated = true;
        isReadyForNextGroup = true;
    }

    public bool Operated => isOperated;
    public bool ReadyForNextGroup => isReadyForNextGroup;

    public int CountPatronGroups()
    {
        return waitingPatronGroups.Count;
    }

    public void RegisterPatronGroup(PatronGroup patronGroup)
    {
        waitingPatronGroups.Add(patronGroup);
    }

    public bool IsRegisteredPatronGroup(PatronGroup patronGroup)
    {
        return waitingPatronGroups.Contains(patronGroup);
    }

    public void UnregisterPatronGroup(PatronGroup patronGroup)
    {
        waitingPatronGroups.Remove(patronGroup);
    }
    
    // Update is called once per frame
    void Update()
    {
        debugTimer += Time.deltaTime;
        if (debugTimer >= 5f)
        {
            AssignTablesToGroups(); //todo: put this in a proper coroutine later
            debugTimer = 0f;
        }
    }

    private void AssignTablesToGroups()
    {
        if (_restaurant.GetPatronGroups().Count == 0)
        {
            return;
        }
        // If there is any group that has no table assigned yet
        // inverting any is faster because it will stop iterating on the first match
        if (_restaurant.GetPatronGroups().All(patronGroup => patronGroup.HasTableAssigned()))
        { 
            // if ALL groups have a table assigned, return.
            return;
        }

        if (!_restaurant.GetTables().Any(table => { return table.IsAvailable(); }))
        {
            //if there are no tables available, we can skip until the next try.
            return;
        }

        // iterate list of all patron groups and filter those without tables
        var waitingGroups = _restaurant.GetPatronGroups().Where(patronGroup => !patronGroup.HasTableAssigned());

        // take a list of all tables that are available and where the number of seats is equal or greater than the number of patrons in the group
        // sort them by number of seats ascending.
        // take the first match.
        foreach (var waitingGroup in waitingGroups)
        {
        var fittingTables = _restaurant.GetTables()
            .Where(table => table.IsAvailable() && table.GetSeatCount() >= waitingGroup.GetPatronCount())
            .OrderBy(table => table.GetSeatCount())
            .ToList();

        // If there is no table that fits, skip this group and go to the next waiting group.
        if (fittingTables.Count == 0) continue;
        
        // assign that table to the group. group will mark itself as assigned and will not show up again
        waitingGroup.AssignTable(fittingTables[0]);
        }
    }
}