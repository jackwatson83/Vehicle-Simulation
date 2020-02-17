using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public GameObject topPanel;
    public GameObject topBackground;
    public GameObject bottomPanel;
    public GameObject bottomBackground;
    public GameObject leftPanel;
    public GameObject leftBackground;
    public GameObject rightPanel;
    public GameObject rightBackground;

    

    public Text vehiclesInputText;
    public Text vehicleNotificationTimeText;
    public Text obstacleMinText;
    public Text obstacleMaxText;
    public Text obstacleNotificationTimeText;

    [Tooltip("This holds the prefab of a notification object, which will be used to populate the notification areas")]
    public GameObject blankNotificationObject;

    [HideInInspector]
    public GameObject activePanel;

    //public List<GameObject> accidentNotifications; legacy

    private void Start()
    {        
        //Begin with no objects activated
        //DeactivateAll();

        //default active panel to the top right
        activePanel = topPanel;
    }    

    /// <summary>
    /// Deactivates all of the notification objects
    /// </summary>
    private void DeactivateAll()
    {
        //legacy:
        //for (int i = 0; i < accidentNotifications.Count; i++)
        //{
        //    accidentNotifications[i].SetActive(false);
        //}

        topPanel.SetActive(false);
        bottomPanel.SetActive(false);
        leftPanel.SetActive(false);
        rightPanel.SetActive(false);

        topBackground.SetActive(false);
        bottomBackground.SetActive(false);
        leftBackground.SetActive(false);
        rightBackground.SetActive(false);

        activePanel.SetActive(false);
    }

    //legacy:

    ///// <summary>
    ///// Activates a specific accident notification.
    ///// </summary>
    ///// <param name="index">identifier for which notification object</param>
    //public void ActivateAccidentNotification(int index)
    //{
    //    accidentNotifications[index].SetActive(true);
    //}

    ///// <summary>
    ///// Deactivates a specific accident notification.
    ///// </summary>
    ///// <param name="index">identifier for which notification object</param>
    //public void DeactivateAccidentNotification(int index)
    //{
    //    accidentNotifications[index].SetActive(false);
    //}


    public void RequestNotificationDisplay(string notificationText, int textSize, Color notificationColour, float lifespan)
    {
        GameObject NO = Instantiate(blankNotificationObject, activePanel.transform);
        NO.gameObject.name = notificationText;
        NO.GetComponent<NotificationObject>().SetColour(notificationColour);
        NO.GetComponent<NotificationObject>().SetText(notificationText, textSize);
        NO.GetComponent<NotificationObject>().SetLifeSpan(lifespan);
    }

    /// <summary>
    /// This method switches the active panel
    /// </summary>
    /// <param name="p">the panel to switch to</param>
    private void PanelSwitch(GameObject p, GameObject backgroundPanel)
    {
        p.SetActive(true);
        //int activeNotifications = activePanel.transform.childCount;
        //for (int i = 0; i < activeNotifications; i++)
        //{
        //    activePanel.transform.GetChild(i).SetParent(p.transform);
        //}

        for (int i=activePanel.transform.childCount - 1; i >=0; --i)
        {
            GameObject n = activePanel.transform.GetChild(i).gameObject;
            n.transform.SetParent(p.transform);
        }

        DeactivateAll();
        backgroundPanel.SetActive(true);
        activePanel = p;
        activePanel.SetActive(true);
    }

    /// <summary>
    /// This method toggles the top panel
    /// </summary>
    public void ToggleTopPanel()
    {
        PanelSwitch(topPanel, topBackground);
    }

    /// <summary>
    /// This method toggles the bottom panel
    /// </summary>
    public void ToggleBottomPanel()
    {
        PanelSwitch(bottomPanel, bottomBackground);
    }

    /// <summary>
    /// This method toggles the left panel
    /// </summary>
    public void ToggleLeftPanel()
    {
        PanelSwitch(leftPanel, leftBackground);
    }

    /// <summary>
    /// This method toggles the right panel
    /// </summary>
    public void ToggleRightPanel()
    {
        PanelSwitch(rightPanel, rightBackground);
    }
}
