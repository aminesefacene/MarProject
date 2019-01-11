using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCar : MonoBehaviour {
    public GameObject car;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        car = GameObject.Find("car");
        offset = new Vector3(0, 0.2f, 0.7f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = car.transform.position + offset;
    }
}
