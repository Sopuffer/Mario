using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Analytics;

public class Timer : MonoBehaviour
{
    StateChange sc;
    public TMP_Text timertext;
    float gametimer = 45;
   
    // Start is called before the first frame update
    void Start()
    {
        sc = GetComponent<StateChange>();
    }

    // Update is called once per frame
    void Update()
    {
        timertext.text = "Time: " + (int)gametimer;

        gametimer -= Time.deltaTime;

        TimesUp();

    }

    void TimesUp()
    {
        if (gametimer <=0)
        {
            sc.PlayerLost();
        }

    }

}
