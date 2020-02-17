using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSpawner : MonoBehaviour
{
    [Header("SETUP")]
    [Tooltip("The number of vehicle objects to add to the object pool on startup")]
    public int numberOfVehicles;
    [SerializeField, Tooltip("The gameobject to hold INACTIVE vehicles")]
    private GameObject inactivePool;
    [SerializeField, Tooltip("The gameobject to hold ACTIVE vehicles")]
    private GameObject activePool;
    [SerializeField, Tooltip("The time to wait (seconds) between vehicle spawns - provided not all vehicles are active")]
    private float waitTime;
    [SerializeField, Tooltip("The Player UI Manager")]
    private PlayerUIManager PlayerUIManager;

    private List<GameObject> vehiclesInScene = new List<GameObject>();

    [Header("Prefab Objects")]
    [SerializeField, Tooltip("The prefab for a Vehicle - child 0 of the prefab should be the model")]
    private GameObject vehiclePrefab;
    [SerializeField, Tooltip("The Vehicle stats for different vehicles. Scriptable Objects for Vehicles go here")]
    private List<VehicleType> vehicleTypes;
    [SerializeField, Tooltip("The default state for vehicles when spawned")]
    private State defaultState;

    [Header("Spawn Information")]
    [SerializeField]
    private Transform[] spawnLocations;
    [SerializeField]
    private Button toggleSpawnButton;

    bool isSpawning;
    static Random RNG = new Random();

    [Header("Other")]
    [SerializeField, Tooltip("The default time notifications appear for")]
    private float notificationTime;
    //notification information (recent spawn location, recent vehicle colour)    
    private Color recentVehicleColour;
    private int recentSpawnLocation;
    

    private void Start()
    {
        //Populate vehicle pool from prefabs
        //LoadVehicles();
        isSpawning = false;
    }

    /// <summary>
    /// sets the number of vehicles to add to the pool
    /// </summary>
    public void SetNumberOfVehiclesInPool()
    {
        //Debug.Log(PlayerUIManager.vehiclesInputText.text);
        numberOfVehicles = int.Parse(PlayerUIManager.vehiclesInputText.text);
    }

    /// <summary>
    /// sets the time that vehicle notifications appear for
    /// </summary>
    public void SetVehicleNotificationTime()
    {
        notificationTime = float.Parse(PlayerUIManager.vehicleNotificationTimeText.text);
    }

    /// <summary>
    /// Button to load the vehicle prefabs into the object pool (inactive)
    /// </summary>
    public void BTN_VehicleSetup()
    {
        LoadVehicles();
    }

    /// <summary>
    /// This method will populate the vehicle pool with Vehicle prefabs.
    /// </summary>
    void LoadVehicles()
    {
        //instantiate x vehicles in the inactive pool, where x is the number of vehicles set in editor
       for (int i = 0; i < numberOfVehicles; i++)
        {
            //GameObject v = Instantiate(vehiclePrefab, inactivePool.transform);
            GameObject v = Instantiate(vehiclePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            v.transform.SetParent(inactivePool.transform);
            vehiclesInScene.Add(v);
        }

       //ensure that all vehicles start off inactive
       foreach (GameObject v in vehiclesInScene)
        {
            v.SetActive(false);
        }
    }

    /// <summary>
    /// Selects a random spawn location from the list
    /// </summary>
    /// <returns>Returns the transform of the selected spawn location</returns>
    Transform SelectSpawnLocation()
    {
        int r = Random.Range(0, spawnLocations.Length - 1);
        recentSpawnLocation = r;
        return spawnLocations[r];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns the first child of the inactive pool, or null if inactive pool is empty</returns>
    GameObject SelectVehicle()
    {
        //GameObject tempVehicle = null;
        //if (inactivePool.transform.childCount != 0)
        //{
        //    tempVehicle = inactivePool.transform.GetChild(0).gameObject;
        //}
        return inactivePool.transform.GetChild(0).gameObject;
    }

    void ActivateVehicle(GameObject vehicle, Transform spawnLocation)
    {
        Vector3 spawnPos = new Vector3(spawnLocation.transform.position.x, spawnLocation.transform.position.y, spawnLocation.transform.position.z);
        vehicle.transform.SetPositionAndRotation(spawnPos, spawnLocation.rotation);

        //plug in vehicle type
        int r = Random.Range(0, vehicleTypes.Count);
        vehicle.GetComponent<VehicleController>().vehicleType = vehicleTypes[r];

        //set the default state
        vehicle.GetComponent<VehicleController>().currentState = defaultState;

        //apply the material to show what vehicle is what - child 0 of the Vehicle prefab should be the model.
        vehicle.transform.GetChild(0).GetComponent<Renderer>().material = vehicle.GetComponent<VehicleController>().vehicleType.vehicleColour;
        //information about colour for notification system
        recentVehicleColour = vehicle.GetComponent<VehicleController>().vehicleType.vehicleColour.color;

        //Set the vehicles first waypoint to the spawn location waypoint
        Waypoint initialWP = spawnLocation.gameObject.GetComponent<Waypoint>();
        vehicle.GetComponent<VehicleController>().currentWaypoint = initialWP;

        //activate the vehicle, and send it on it's merry way.
        vehicle.transform.SetParent(activePool.transform);
        vehicle.SetActive(true);
        vehicle.GetComponent<VehicleController>().SetDefaultSpeed();
        vehicle.GetComponent<VehicleController>().SetCurrentSpeed(vehicle.GetComponent<VehicleController>().vehicleDefaultSpeed);
        //vehicle.GetComponent<VehicleController>().vehicleCamera.enabled = false;
        vehicle.GetComponent<VehicleController>().ActivateAI();
    }

    /// <summary>
    /// This method is tied to a button on the pause menu, and will toggle vehicle spawning on or off
    /// </summary>
    public void ToggleSpawning()
    {
        //toggle is spawning to the opposite
            //ie if it's true, become false, if it's false, become true
        isSpawning = !isSpawning;
        StartCoroutine("VehicleSpawn");
    }

    /// <summary>
    /// Displays a notification on the Player's UI
    /// </summary>
    private void DisplayNewVehicleInformation()
    {
        PlayerUIManager.RequestNotificationDisplay("New Vehicle: Junction (" + recentSpawnLocation + ")", 10, recentVehicleColour, notificationTime);
    }

    IEnumerator VehicleSpawn()
    {
        while(isSpawning)
        {
            if (inactivePool.transform.childCount != 0)
            {
                ActivateVehicle(SelectVehicle(), SelectSpawnLocation());
                DisplayNewVehicleInformation();
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
