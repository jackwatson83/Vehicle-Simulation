using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traffic/Vehicles/Blank Vehicle Template")]
public class VehicleType : ScriptableObject
{
    [Tooltip("The name of this vehicle type")]
    public string vehicleClass;

    [Tooltip("The default speed of this vehicle type")]
    public float speed;

    [Tooltip("The acceleration for this vehicle type")] //currently unused, but would become relevant with more vehicle types and more intricate behaviours.
    public float acceleration;

    [Tooltip("The 'driver' vision uses a sphere cast. Set the radius of that sphere here.")]
    public float driverViewRadius;

    [Tooltip("The distance the 'driver' can see. This goes forwards, and in a straight line, and will be drawn as a ray in debug.")]
    public float driverViewDistance;

    public Material vehicleColour;
}
