using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Waypoint : MonoBehaviour
{
    [Header("Gizmos")]
    [SerializeField, Tooltip("Colour of Gizmo")]
    private Color gizmoColour;
    [SerializeField, Tooltip("Radius of Gizmo Sphere")]
    private float sphereRadius;

    [Space]
    [Header("Connecting Waypoints")]
    [SerializeField, Tooltip("The waypoints this connects to")]
    List<Waypoint> connections;

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColour;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
        if (connections.Count != 0)
        {
            foreach (Waypoint w in connections)
            {
                Gizmos.DrawLine(transform.position, w.gameObject.transform.position);
            }
        }
        
    }

    /// <summary>
    /// Method to be called when this waypoint is reached.
    /// </summary>
    /// <returns>Returns the next waypoint to move to</returns>
    public Waypoint NextWaypoint(VehicleController controller)
    {
        Waypoint temp = new Waypoint();

        if (controller.isPlayer)
        {
            //If the vehicle is the Player, we don't want that vehicle to leave the road.
            //Exists will never be the first connection (from the main road loop), so we can cheat here.
            temp = connections[0];
        }
        else
        {
            if (connections.Count == 1)
            {
                temp = connections[0];
            }
            else if (connections.Count == 2)
            {
                if (Random.value <= 0.5f)
                {
                    temp = connections[0];
                }
                else
                {
                    temp = connections[1];
                }
            }
            else
            {
                Debug.Log("Waypoint count invalid");
            }
        }

        return temp;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The transform of the waypoint gameobject</returns>
    public Transform GetWaypointTransform()
    {
        return this.gameObject.transform;
    }
    
}
