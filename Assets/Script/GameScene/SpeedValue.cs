using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedValue : MonoBehaviour {

    public Text t = null;
    private GameObject car;
    private Vector3 lastPosition;
    private float speed;

    // Use this for initialization
    void Start () {
        car = GameObject.Find("car");
        lastPosition = car.GetComponent<Rigidbody>().transform.position;
    }


    void FixedUpdate()
    {
        speed = (transform.position - lastPosition).magnitude*100;
        t.text = speed.ToString();
        lastPosition = transform.position;
    }
}
