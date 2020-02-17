using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : ScriptableObject
{
    /// <summary>
    /// This is the base Decide method, overridden in each specific type of Decision.
    /// Use to call a function within the child of Decision
    /// </summary>
    /// <param name="controller">The vehicle controller, which functions as the control of the vehicle state machine</param>
    /// <returns>Returns a true or false bool, to identify whether the decision was successful or not</returns>
    public abstract bool Decide(VehicleController controller);
}
