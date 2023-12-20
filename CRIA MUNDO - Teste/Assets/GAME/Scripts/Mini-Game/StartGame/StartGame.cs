using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGame : MonoBehaviour
{
    public float countdownTime = 5f;
    public TextMeshProUGUI countdownDisplay;

    private float currentTime;
    private bool timerIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = countdownTime;
        timerIsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsActive == true)
        {
            currentTime -= Time.deltaTime;
            timerIsActive = true;
        }

        if (countdownDisplay != null)
        {
            countdownDisplay.text = currentTime.ToString("F2");
        }

        if (currentTime <= 0)
        {
            timerIsActive = false;
            currentTime = 0;

            TimerFinished();
        }
    }

    private void TimerFinished()
    {


    }
}


