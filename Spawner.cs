using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("SETUP")]
    [Tooltip("The number of cars to add to the object pool on startup")]
    public int numberOfCars;
    [Tooltip("The number of lorries to add to the object pool on startup")]
    public int numberOfLorries;
    [SerializeField, Tooltip("The gameobject to hold inactive vehicles")]
    private GameObject inactivePool;
    [SerializeField, Tooltip("The gameobject to hold active vehicles")]
    private GameObject activePool;
    [SerializeField, Tooltip("The time to wait (seconds) between vehicle spawns")]
    private float waitTime;

    private List<GameObject> vehiclesInScene = new List<GameObject>();
    /// <summary>
    /// This bool is used to dictate the direction of travel. TRUE represents NORTH, FALSE represents SOUTH
    /// </summary>
    private bool vehicleDirection;

    [Header("Prefab Objects")]
    [SerializeField, Tooltip("The prefab for a car")]
    private GameObject carPrefab;
    [SerializeField, Tooltip("The prefab for a lorry")]
    private GameObject lorryPrefab;

    [Header("Materials")]
    [SerializeField]
    private Material[] carMaterials;
    [SerializeField]
    private Material[] lorryMaterials;

    [Header("Individual Lane spawn transforms")]
    [SerializeField]
    private Transform insideNORTH;
    [SerializeField]
    private Transform middleNORTH;
    [SerializeField]
    private Transform outsideNORTH;
    [SerializeField]
    private Transform insideSOUTH;
    [SerializeField]
    private Transform middleSOUTH;
    [SerializeField]
    private Transform outsideSOUTH;

    bool isSpawning;

    private void Start()
    {
        //load in the vehicle pool, using the prefabs
        LoadVehicles();
        isSpawning = false;
    }


    // Update is called once per frame
    void Update()
    {
         
    }

    public void BTNStartSpawning()
    {
        //Write a coroutine to use WaitForSeconds()
        //After x seconds (vary this value, so it isn't regular):
        //  if inactive pool isn't empty
        //  Activate a vehicle from the Inactive Pool, and move it to the Active Pool
        //  Randomly decide a lane and direction for the vehicle to be in
        //  Assign the vehicle a random colour/material (to spice things up, as well as for testing purposes)
        //  Put the vehicle there, and ensure it is travelling in the correct direction - requires changing the vehicle script to not use Vector3.forward
        //      Instead, have a bool to toggle which direction the vehicle is travelling in
        isSpawning = true;
        StartCoroutine("VehicleSpawn");
        
    }

    /// <summary>
    /// This method will load the correct amount of vehicles into the inactive pool
    /// </summary>
    void LoadVehicles()
    {
        //Load Cars
        for (int i = 0; i < numberOfCars; i++)
        {
            GameObject c = Instantiate(carPrefab, inactivePool.transform);
            c.transform.SetParent(inactivePool.transform);
            vehiclesInScene.Add(c);
        }

        //Load Lorries
        for (int i = 0; i < numberOfLorries; i++)
        {
            GameObject l = Instantiate(lorryPrefab, inactivePool.transform);
            l.transform.SetParent(inactivePool.transform);
            vehiclesInScene.Add(l);
        }

        //ensure vehicle gameobjects are not active
        foreach (GameObject v in vehiclesInScene)
        {
            v.SetActive(false);
        }

    }

    GameObject SelectVehicle()
    {
        //Random number to select a child in the inactive pool
        int r = Random.Range(0, inactivePool.transform.childCount - 1);
        return inactivePool.transform.GetChild(r).gameObject;        
    }

    Transform SelectLane()
    {
        int r = Random.Range(0, 5);
        switch(r)
        {
            default: //default will be the outside northbound lane
            case 0:
                vehicleDirection = false;
                return outsideNORTH.transform;
            case 1:
                vehicleDirection = false;
                return middleNORTH.transform;
            case 2:
                vehicleDirection = false;
                return insideNORTH.transform;
            case 3:
                vehicleDirection = true;
                return outsideSOUTH.transform;
            case 4:
                vehicleDirection = true;
                return middleSOUTH.transform;
            case 5:
                vehicleDirection = true;
                return insideSOUTH.transform;
        }
    }

    void ActivateVehicle(GameObject vehicle, Transform lane)
    {
        //move vehicle to correct lane (including orientation)
        Vector3 lanePos = new Vector3(lane.transform.position.x, lane.transform.position.y, lane.transform.position.z);
        Debug.Log("lane: " + lane.transform.position);
        vehicle.transform.SetPositionAndRotation(lanePos, lane.transform.rotation);
        Debug.Log("vehicle: " + vehicle.transform.position);

        //vehicle.transform.position = lane.transform.position;

        //set the vehicle's direction
        //Vehicle v = vehicle.GetComponent<Vehicle>();
        //if (v != null)
        //{
        //    v.SetDirection(vehicleDirection);


        //}

        //put the vehicle in the active pool
        vehicle.transform.SetParent(activePool.transform);
        //colour the vehicle - will need to include a check for what type of vehicle (when Vehicle.cs is reworked)
        vehicle.transform.GetChild(0).GetComponent<Renderer>().material = carMaterials[Random.Range(0, carMaterials.Length - 1)];
        //activate the vehicle
        vehicle.SetActive(true);
    }

    IEnumerator VehicleSpawn()
    {
        while(isSpawning)
        {
            if (inactivePool.transform.childCount != 0)
            {
                ActivateVehicle(SelectVehicle(), SelectLane());
            }
            yield return new WaitForSeconds(waitTime);
        }
               
    }
}
