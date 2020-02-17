using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Traffic/Actions/Wait")]
public class WaitAction : Action
{
    public override void Act(VehicleController controller)
    {
        Wait(controller);
    }

    /// <summary>
    /// This method stops the agent from moving, and sets the current speed to 0.
    /// It is important to set the speed to 0, since vehicles behind this will not react to an obstacle (and enter the Waiting State),
    ///     instead they will see and copy the speed of a stopped vehicle.
    /// </summary>
    /// <param name="controller"></param>
    private void Wait(VehicleController controller)
    {
        controller.SetCurrentSpeed(0);
        controller.navMeshAgent.isStopped = true;
    }
}
