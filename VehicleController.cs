using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleController : MonoBehaviour
{
    [Tooltip("Identifies the vehicle as the player - this vehicle will be permanently")]
    public bool isPlayer;

    public VehicleType vehicleType;
    public State currentState;
    public State remainState;
    [Tooltip("This represents where the driver is, and is the position where the agent 'looks' from.")]
    public Transform driver;
    [Tooltip("Each vehicle has it's own camera - this is used to represent the view if the player is driving the selected vehicle")]
    public Camera vehicleCamera;
    [SerializeField, Tooltip("The collider component of this vehicle")]
    private Collider vehicleCollider;

    [SerializeField, Tooltip("The speed of this vehicle. Initially set as the default speed, from the VehicleType ScriptableObject")]
    private float vehicleCurrentSpeed;
    [SerializeField, Tooltip("The default speed of the vehicle")]
    public float vehicleDefaultSpeed;

    /// <summary>
    /// This variable is used to identify another vehicle in front of this.
    /// In most cases, this will be used to find out the speed of the other vehicle, to allow speed-matching.
    /// </summary>
    private GameObject vehicleInfront;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public float stateTimeElapsed;
    //[HideInInspector]
    public Waypoint currentWaypoint;

    /// <summary>
    /// This bool activates the AI of the agent -> used to begin the navigation, and turned off when the gameobject is inactive.
    /// </summary>
    [SerializeField]private bool aiActive;

    private void Awake()
    {
        NavMeshAgent tempAgent = GetComponent<NavMeshAgent>();
        if (tempAgent == null)
        {
            Debug.Log("VEHICLE CONTROLLER: This object requires a Nav Mesh Agent Component");
        }
        else
        {
            navMeshAgent = tempAgent;
        }

        //SetDefaultSpeed();
        //SetCurrentSpeed(vehicleDefaultSpeed);
        SetDriver();
    }

    private void Update()
    {
        if (!aiActive) { return; }

        currentState.UpdateState(this);
    }

    private void OnDrawGizmos()
    {
        if (currentState != null && driver != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(
                driver.position, 
                vehicleType.driverViewRadius);
        }
    }

    /// <summary>
    /// Changes the current state of the State Machine
    /// </summary>
    /// <param name="nextState">The State to transition to</param>
    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    /// <summary>
    /// This method allows for decisions to be time based. 
    /// </summary>
    /// <param name="duration">Float to represent the desired time of the countdown</param>
    /// <returns>Returns true if the timer has reached the duration.</returns>
    public bool CheckIfCountdownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    /// <summary>
    /// When the state changes, the time elapsed must be reset.
    /// </summary>
    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    /// <summary>
    /// Turns on the AI component, so that navigation begins
    /// </summary>
    public void ActivateAI()
    {
        if (!aiActive)
        {
            aiActive = true;
        }
    }

    /// <summary>
    /// Turns off the AI component, to stop navigation
    /// </summary>
    public void DeactivateAI()
    {
        if (aiActive)
        {
            aiActive = false;
        }
    }

    /// <summary>
    /// This sets the vehicle's speed to the default (which is found in the ScriptableObject of specific VehicleTypes)
    /// </summary>
    public void SetDefaultSpeed()
    {
        //When the object pool is populated when the game starts, the vehicle type field won't yet be set, so this check is required.
        if (vehicleType != null)
        {
            vehicleDefaultSpeed = vehicleType.speed;
        }        
    }

    /// <summary>
    /// This allows you to set the current speed to any float value. Current speed is the speed that the vehicle moves at.
    /// </summary>
    /// <param name="desiredSpeed">The desired speed.</param>
    public void SetCurrentSpeed(float desiredSpeed)
    {
        vehicleCurrentSpeed = desiredSpeed;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>The current speed of the vehicle</returns>
    public float GetCurrentSpeed()
    {
        return vehicleCurrentSpeed;
    }

    /// <summary>
    /// This method returns the speed of another vehicle.
    /// The other vehicle will be found using the "LookForward" Decision
    /// </summary>
    /// <returns>The speed of the vehicle in front of this one.</returns>
    public float FindSpeedToMatch()
    {
        float otherSpeed = vehicleInfront.GetComponent<VehicleController>().GetCurrentSpeed();
        if (otherSpeed <= vehicleCurrentSpeed)
        {
            vehicleCurrentSpeed = otherSpeed;
            return otherSpeed;
        }
        else
        {
            return vehicleCurrentSpeed;
        }         
    }

    /// <summary>
    /// Sets the vehicle in front. Called from the Look Decision.
    /// </summary>
    /// <param name="vehicle">the vehicle in front</param>
    public void SetVehicleInFront(GameObject vehicle)
    {
        vehicleInfront = vehicle;
    }

    /// <summary>
    /// This runs during the intial population of the object pool.
    /// The driver is the second child of the vehicle prefab.
    /// </summary>
    private void SetDriver()
    {
        driver = transform.GetChild(1);
    }

    /// <summary>
    /// Toggles the AI if this instance is the player vehicle.
    /// </summary>
    public void TogglePlayerAI()
    {
        if (isPlayer)
        {
            aiActive = !aiActive;
        }
    }
}
