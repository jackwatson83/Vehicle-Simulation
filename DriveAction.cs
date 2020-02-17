using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traffic/Actions/Drive")]
public class DriveAction : Action
{
    public override void Act(VehicleController controller)
    {
        Drive(controller);
    }

    /// <summary>
    /// This method handles the basic driving action of each vehicle.
    /// </summary>
    /// <param name="controller">The vehicle controller, which functions as the control of the vehicle state machine</param>
    private void Drive(VehicleController controller)
    {
        //If the navmesh agent is stopped when this is first called, set the speed to the default speed
        //Otherwise, get the current speed from the vehicle controller
        //  This happens after coming from the Waiting State
        if (controller.navMeshAgent.isStopped == false)
        {
            controller.navMeshAgent.speed = controller.vehicleDefaultSpeed;
        }
        else
        {
            controller.navMeshAgent.speed = controller.GetCurrentSpeed();
        }
        //Debug.Log("Driving");
        
        //Debug.Log(controller.navMeshAgent.speed);
        controller.navMeshAgent.destination = controller.currentWaypoint.transform.position;
        //Debug.Log(controller.navMeshAgent.destination);
        controller.navMeshAgent.isStopped = false;
        //Debug.Log(controller.navMeshAgent.isStopped);

        if(controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            //Get the waypoint after the current one
            controller.currentWaypoint = controller.currentWaypoint.NextWaypoint(controller);
        }

        //rotate to face movement of travel correctly -> https://forum.unity.com/threads/solved-navmesh-agent-instant-turn-in-direction-he-moves.521794/
        if (controller.navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            controller.transform.rotation = Quaternion.LookRotation(controller.navMeshAgent.velocity.normalized);
        }
    }
}
