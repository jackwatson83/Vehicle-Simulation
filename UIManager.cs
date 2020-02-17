using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField, Tooltip("The panel to display with game settings, ")]
    private GameObject settingsPanel;

    [SerializeField, Tooltip("The initial settings panel")]
    public GameObject initialPanel;

    // Start is called before the first frame update
    void Start()
    {
        initialPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsPanelToggle();
        }
    }

    /// <summary>
    /// toggles the intial settings panel
    /// </summary>
    public void ToggleInitialPanel()
    {
        if (initialPanel.activeInHierarchy)
        {
            initialPanel.SetActive(false);
        }
        else
        {
            initialPanel.SetActive(true);
        }
    }

    /// <summary>
    /// toggles the in game settings panel
    /// </summary>
    void SettingsPanelToggle()
    {
        if(settingsPanel.activeInHierarchy)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(true);
        }
    }

    /// <summary>
    /// closes the application
    /// </summary>
    public void CloseApplication()
    {
        Application.Quit();
    }    
}
