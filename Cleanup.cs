using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{
    [SerializeField, Tooltip("The pool of vehicles to return objects to.")]
    private GameObject vehiclePool;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        //Return the other object to the object pool for re use
        //Requires getting the empty parent object, otherwise just the model with collider will be disabled and re-parented.
        GameObject otherParent = other.transform.parent.gameObject;
        otherParent.GetComponent<VehicleController>().DeactivateAI();
        otherParent.gameObject.SetActive(false);
        otherParent.transform.SetParent(vehiclePool.transform);
    }
}
