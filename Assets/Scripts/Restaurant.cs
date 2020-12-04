using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Restaurant : Singleton<Restaurant>
{
    private float currentTime = 0f;
    private float dayLength = 24f;
    private int day = 0;
    
    private List<Table> tables = new List<Table>();
    private List<PatronGroup> patronGroups = new List<PatronGroup>();

    protected Restaurant() { }
    
    void Start()
    {
    }

    public float GetDayLength()
    {
        return dayLength;
    }
    public int GetCurrentDay()
    {
        return day;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public void RegisterTable(Table newTable)
    {
        tables.Add(newTable);
    }

    public List<PatronGroup> GetPatronGroups()
    {
        return patronGroups;
    }

    public List<Table> GetTables()
    {
        return tables;
    }

    public void RegisterPatronGroup(PatronGroup patronGroup)
    {
        patronGroups.Add(patronGroup);
    }

    public bool IsRegistered(PatronGroup patronGroup)
    {
        return patronGroups.Contains(patronGroup);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= dayLength)
        {
            currentTime = currentTime % dayLength;
            day++;
        }
    }
}

public class AssignTableToPatronGroupEventArgs : EventArgs
{
    public AssignTableToPatronGroupEventArgs(PatronGroup group, Table table)
    {
        Table = table;
        Group = group;
    }
    
    public Table Table { get; set; }
    public PatronGroup Group { get; set; }
}