using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Traffic/Decisions/LookForObstacle")]
public class LookForObstacleDecision : Decision
{
    public override bool Decide(VehicleController controller)
    {
        return Look(controller);
    }

    private bool Look(VehicleController controller)
    {
        RaycastHit h;

        Debug.DrawRay(controller.driver.position, controller.driver.forward.normalized * controller.vehicleType.driverViewDistance, Color.red);
        //Check if the vehicle can see an obstacle, return true if we can and false if not.
        if (Physics.SphereCast(controller.driver.position, controller.vehicleType.driverViewRadius, controller.driver.forward, out h, controller.vehicleType.driverViewDistance) && h.collider.transform.parent.CompareTag("Obstacle"))
        {            
            return true;
        }
        else
        {            
            return false;
        }
    }
}
