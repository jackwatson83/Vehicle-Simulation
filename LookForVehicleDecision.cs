using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Traffic/Decisions/LookForVehicle")]
public class LookForVehicleDecision : Decision
{
    public override bool Decide(VehicleController controller)
    {
        bool vehicleInFront = Look(controller);
        return vehicleInFront;
    }

    private bool Look(VehicleController controller)
    {
        RaycastHit h;

        //Debug.Log("looking " + controller.driver.position + " "+ controller.vehicleType.driverViewDistance);

        //Draw a ray to show where the 'driver' is looking, and how far it can see
        Debug.DrawRay(controller.driver.position, controller.driver.forward.normalized * controller.vehicleType.driverViewDistance, Color.green);

        if (Physics.SphereCast(controller.driver.position, controller.vehicleType.driverViewRadius, controller.driver.forward, out h, controller.vehicleType.driverViewDistance) && h.collider.transform.parent.CompareTag("Vehicle"))
        {
            //If we see a vehicle, set the vehicleInfront variable
            controller.SetVehicleInFront(h.transform.parent.gameObject);
            return true;
        }
        else
        {
            //If we can't see a vehicle in front, clear the variable
            controller.SetVehicleInFront(null);
            return false;
        }
    }
}
