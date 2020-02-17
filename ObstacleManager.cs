using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField, Tooltip("The obstacles that can be generated. These will be in set locations, and just activated when necessary.")]
    private List<GameObject> obstacles;

    [SerializeField, Tooltip("The MIN time to wait (seconds) between obstacle spawns")]
    private float waitTimeMin;
    [SerializeField, Tooltip("The MAX time to wait (seconds) between obstacle spawns")]
    private float waitTimeMax;
    [SerializeField, Tooltip("The length of time a notification appears for on the UI")]
    private float notificationTime;

    [SerializeField, Tooltip("The Player UI Manager")]
    private PlayerUIManager PlayerUIManager;

    bool isSpawning;

    private void Start()
    {
        //Begin with all obstacles deactivated
        DeactivateAll();
        //spawning is off by default
        isSpawning = false;
    }

    /// <summary>
    /// sets the min wait time between obstacle spawns
    /// </summary>
    public void SetMinWait()
    {
        waitTimeMin = float.Parse(PlayerUIManager.obstacleMinText.text);
    }

    /// <summary>
    /// sets the max wait time between obstacle spawns
    /// </summary>
    public void SetMaxWait()
    {
        waitTimeMax = float.Parse(PlayerUIManager.obstacleMinText.text);
    }

    /// <summary>
    /// sets the time that obstacle notifications appear for
    /// </summary>
    public void SetNotificationTime()
    {
        notificationTime = float.Parse(PlayerUIManager.obstacleNotificationTimeText.text);
    }

    /// <summary>
    /// Activates a desired obstacle
    /// </summary>
    /// <param name="index"></param>
    private void ActivateObstacle(int index)
    {
        obstacles[index].SetActive(true);
    }

    /// <summary>
    /// Deactivates a desired obstacle
    /// </summary>
    /// <param name="index"></param>
    private void DeactivateObstacle(int index)
    {
        obstacles[index].SetActive(false);
    }

    private void ToggleObstacle(int index)
    {
        if (obstacles[index].activeSelf)
        {
            DeactivateObstacle(index);
            //PlayerUIManager.DeactivateAccidentNotification(index);
            
        }
        else
        {
            ActivateObstacle(index);
            //PlayerUIManager.ActivateAccidentNotification(index);
            PlayerUIManager.RequestNotificationDisplay("Accident(" + index + ")", 12, Color.red, notificationTime);
        }
    }

    /// <summary>
    /// Deactivates all the obstacles
    /// </summary>
    private void DeactivateAll()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].SetActive(false);
        }
    }

    /// <summary>
    /// This method is tied to a button on the pause menu, and will toggle obstacle spawning on or off
    /// </summary>
    public void ToggleSpawning()
    {
        isSpawning = !isSpawning;
        StartCoroutine("ObstacleSpawn");
    }

    /// <summary>
    /// Randomly decides a time to wait between activations, but each time activated, toggles one of the obstacles in the scene.
    /// </summary>
    /// <returns></returns>
    IEnumerator ObstacleSpawn()
    {
        while(isSpawning)
        {
            Debug.Log("Obstacle check");
            ToggleObstacle(Random.Range(0, obstacles.Count - 1));
            yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));
        }
    }
}
