using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Traffic/Actions/Follow")]
public class FollowAction : Action
{
    public override void Act(VehicleController controller)
    {
        Follow(controller);
    }

    /// <summary>
    /// This is functionally the same as the Drive Action, but it takes the speed from the vehicle in front.
    /// </summary>
    /// <param name="controller"></param>
    private void Follow(VehicleController controller)
    {
        //The only difference between this and drive action is to match the speed to the vehicle in front of this one.
        controller.navMeshAgent.speed = controller.FindSpeedToMatch();

        controller.navMeshAgent.destination = controller.currentWaypoint.transform.position;
        controller.navMeshAgent.isStopped = false;

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            controller.currentWaypoint = controller.currentWaypoint.NextWaypoint(controller);
        }
    }
}
