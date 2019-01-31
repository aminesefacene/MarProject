using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayGhost : MonoBehaviour {

    public GameObject car;
    public bool ghostMode;
    public List<Vector3> ghostPosition;
    public List<Quaternion> ghostRotation;

    // Use this for initialization
    void Start () {
        car = GameObject.Find("car");
        ghostMode = true;
        if (ghostMode)
        {
            ghostPosition = new List<Vector3>();
            ghostRotation = new List<Quaternion>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (ghostMode)
        {
            Debug.Log(car.GetComponent<Rigidbody>().position);
            ghostPosition.Add(car.GetComponent<Rigidbody>().position);
            ghostRotation.Add(car.GetComponent<Rigidbody>().rotation);
        }
    }
}
