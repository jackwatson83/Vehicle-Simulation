using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationObject : MonoBehaviour
{
    [SerializeField]
    private Image background;
    [SerializeField]
    private Text textBox;

    private float lifespan;

    private void Update()
    {
        Timer();
    }

    /// <summary>
    /// sets the colour of the notification
    /// </summary>
    /// <param name="c">desired colour</param>
    public void SetColour(Color c)
    {
        background.color = c;
    }

    /// <summary>
    /// Sets the text of the notification
    /// </summary>
    /// <param name="t">text content</param>
    /// <param name="s">text size</param>
    public void SetText(string t, int s)
    {
        textBox.text = t;
        textBox.fontSize = s;
    }

    /// <summary>
    /// Sets a lifespan of the notification, after which it destroys itself
    /// </summary>
    /// <param name="f"></param>
    public void SetLifeSpan(float f)
    {
        lifespan = f;
    }

    /// <summary>
    /// Destroys the object after x seconds, where x is the lifespan
    /// </summary>
    private void Timer()
    {
        //Avoid using destroy in virtually every situation
        //used here as a quick fix
        Destroy(this.gameObject, lifespan);
    }
}
