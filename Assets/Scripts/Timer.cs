using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    StateChange sc;
    public TMP_Text timertext;
    float gametimer = 76;
   
    void Start()
    {
        sc = GetComponent<StateChange>();
   
    }

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
