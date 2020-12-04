using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationChangedEventArgs
{
    public Vector3 NewLocation { get; }
    public LocationChangedEventArgs(Vector3 setNewLocation)
    {
        NewLocation = setNewLocation;
    }
}

public class TableAssignEventArgs
{
    public Table AssignedTable { get; }

    public TableAssignEventArgs(Table table)
    {
        AssignedTable = table;
    }
}
public class PatronGroup : MonoBehaviour
{
    public delegate void LocationChangeHandler(object sender, LocationChangedEventArgs eventArgs);
    public event LocationChangeHandler LocationChange = delegate { };

    public delegate void TableAssignHandler(object sender, TableAssignEventArgs eventArgs);
    public event TableAssignHandler TableAssign = delegate { };
    
    [SerializeField] int spawnCount = 1;
    [SerializeField] private GameObject patronPrefab;
    private GroupState state;
    private Vector3 groupLocation;
    private float groupLocationBounds = 3f;
    private Restaurant restaurant;
    private Table assignedTable;
    private bool isSpawned = false;

    private readonly List<Patron> patrons = new List<Patron>();


    private void Awake()
    {
        restaurant = Restaurant.Instance;
    }

    public bool HasTableAssigned()
    {
        return assignedTable != null;
    }

    public Vector3 GetLocation()
    {
        return groupLocation;
    }

    public float GetLocationBounds()
    {
        return groupLocationBounds;
    }

    public Table GetAssignedTable()
    {
        return assignedTable;
    }

    private void Update()
    {
        
    }

    public GroupState GetState()
    {
        return state;
    }
    
    public void Spawn()
    {
        // don't spawn if the number of patrons is 0 or they are already spawned
        
        if (spawnCount == 0 || isSpawned) return;

        restaurant.RegisterPatronGroup(this);
        
        // position group object at the entry object
        var entry = FindObjectOfType<Entry>();
        transform.position = entry.transform.position;
        groupLocation = transform.position;
        
        // initial state
        state = GroupState.PassingBy;
        
        for (var numSpawned = 0; numSpawned < spawnCount; numSpawned++)
        {
            var instance = Instantiate(patronPrefab, transform.parent);
            var patron = instance.GetComponent<Patron>();
            patron.SetGroup(this);
            patrons.Add(patron);

            if (HasTableAssigned() && assignedTable.GetSeats().Any(seat => seat.IsAvailable()))
            {
                var openSeat = assignedTable.GetSeats().First(seat => seat.IsAvailable());
                patron.AssignSeat(openSeat);
            }
        }
    }

    public void SetSpawnAmount(int spawnAmount)
    {
        spawnCount = spawnAmount;
    }

    
    void ChangeLocation(Vector3 newLocation)
    {
        groupLocation = newLocation;
        LocationChange?.Invoke(this, new LocationChangedEventArgs(newLocation));
    }

    public void AssignTable(Table table)
    {
        assignedTable = table;
        table.SetAvailability(false);

        AssignSeatsToGroupMembers();
        
        ChangeLocation(table.transform.position);
        TableAssign?.Invoke(this, new TableAssignEventArgs(table));
    }

    private void AssignSeatsToGroupMembers()
    {
        foreach (var patron in patrons)
        {
            var  newSeat = assignedTable.GetSeats().First(seat => seat.IsAvailable());
            patron.AssignSeat(newSeat);
        }
    }

    public int GetPatronCount()
    {
        return patrons.Count;
    }

    private void OnDestroy()
    {
        if (assignedTable is null)
        {
            return;
            
        }
        foreach (var seat in assignedTable.GetSeats())
        {
            seat.SetAvailability(true);
        }
        assignedTable.SetAvailability(true);
    }
}


