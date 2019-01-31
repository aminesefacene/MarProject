using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour {

    public Text t = null;
    public GameObject timeManager;
    public string time;

    void Start () {
        time = timeManager.GetComponent<TimeManager>().GetTimer().ToString();
    }

    void Update()
    {
        t.text = time;
    }
}
