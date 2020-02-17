using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ~LEGACY CODE~
//  This script was used to check if an agent had reached the final destination.
//  An early rework changed the road map to have varying end points, so I changed how agents finished their journeys.

//[CreateAssetMenu(menuName = "Traffic/Decisions/DestinationReached")]
public class DestinationReachedDecision : Decision
{
    public override bool Decide(VehicleController controller)
    {
       return DestinationReached(controller);
    }

    private bool DestinationReached(VehicleController controller)
    {
        bool decision = false;

        if (controller.currentWaypoint.gameObject.name == "END" && controller.navMeshAgent.remainingDistance == 0)
        {
            Debug.Log("destination reached");
            decision = true;
        }

        return decision;
    }
}
