using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    /// <summary>
    /// This is the base Act method, to be overridden in each specific action.
    /// Call a function within this from each child of Action.
    /// </summary>
    /// <param name="controller">The vehicle controller, which functions as the control of the vehicle state machine</param>
    public abstract void Act(VehicleController controller);
}
