using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapTimer : MonoBehaviour {

    public bool endableTimer = false;

    public int levelTime = 25;
    int currentTime = 0;

    public Text timerText;
    public GameObject gameoverUI;

    private void Awake()
    {
        if(endableTimer)
        {
            timerText.text = levelTime.ToString();
            currentTime = levelTime;
        }
        else
        {
            //setting timer text to nothing, if timer not enabled
            timerText.text = "";
        }

    }

    private void Start()
    {
        //counting down timer by 1 second, with a delay of 1 second
        if(endableTimer)
        {
            InvokeRepeating("updateTime", 1f, 1f);
        }
    }

    public void updateTime()
    {
        if (currentTime == 0)
        {
            timerEnded();
        }
        else
        {
            currentTime -= 1;
            timerText.text = currentTime.ToString();
        }
    }

    public void timerEnded()
    {
        Debug.Log("timerEnded - End Level");
        //players lost
        //disable player controls
        //show end game UI
    }
}
