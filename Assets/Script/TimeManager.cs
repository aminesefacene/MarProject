using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public static float timer;

    public void StartTimer()
    {
        timer = 0f;
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        //Debug.Log("timer : " + timer);
    }

    public float GetTimer()
    {
        return timer;
    }
}
