using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReplayMode : MonoBehaviour {

    public GameObject car;

	void Start () {
        car = GameObject.Find("car");
    }
	
	void Update ()
    {
        transform.LookAt(car.transform);
        /*if (car.GetComponent<Renderer>())
        {

        }*/
    }
}
