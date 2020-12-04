using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Patron : MonoBehaviour
{
    
    private PatronState state;
    private Restaurant restaurant;

    // Main Game Object
    private Seat assignedSeat;
    private bool hasSeatAssigned = false;
    private NavMeshAgent navAgent;
    private PatronGroup patronGroup;
    
    private IPatronBehaviour behaviour;
    private float stuckTimer = 0f;

    public void SetGroup(PatronGroup newGroup)
    {
        patronGroup = newGroup;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        restaurant = Restaurant.Instance;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoRepath = true;
    }

    public void AssignSeat(Seat seat)
    {
        assignedSeat = seat;
        hasSeatAssigned = true;
        seat.SetAvailability(false);
    }

    void Update()
    {
        if (navAgent.remainingDistance > 1f)
        {
            var combinedVelocity = Mathf.Abs(navAgent.velocity.x) + Mathf.Abs(navAgent.velocity.y) +
                                   Mathf.Abs(navAgent.velocity.z);
            var combinedDesiredVelocity = Mathf.Abs(navAgent.desiredVelocity.x) + Mathf.Abs(navAgent.desiredVelocity.y) +
                Mathf.Abs(navAgent.desiredVelocity.z);

            if (combinedVelocity < combinedDesiredVelocity / 10)
            {
                stuckTimer += Time.deltaTime;
                if (stuckTimer >= 4f)
                {
                    Debug.Log("IM STUCK");
                    navAgent.velocity = new Vector3(Random.Range(-5f,5f), Random.Range(-5f,5f), 0);
                    stuckTimer = 0f;
                }

            }
        }

        navAgent.stoppingDistance = 1.8f;
       //behaviour.Behave();
       if (patronGroup.HasTableAssigned() && hasSeatAssigned)
       {
           navAgent.stoppingDistance = 0.05f;
           navAgent.destination = assignedSeat.transform.position;
       }
       else
       {
           var randomCirclePos = Random.insideUnitCircle * patronGroup.GetLocationBounds();
           navAgent.destination = patronGroup.GetLocation();
       }
    }

}
