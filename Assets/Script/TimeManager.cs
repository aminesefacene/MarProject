using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public static float timer;
    public static int mode;

    public void StartTimer()
    {
        timer = 0f;
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

    public float GetTimer()
    {
        return timer;
    }

    public void SetMode(int m)
    {
        mode = m;
    }

    public int GetMode()
    {
        return mode;
    }
}
