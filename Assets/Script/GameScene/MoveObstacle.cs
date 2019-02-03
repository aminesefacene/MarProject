using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour {

    private float cptX;
    private float min;
    private float max;
    private bool b;

    // Use this for initialization
    void Start () {
        min = cptX - 3;
        max = cptX + 3;
        b = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (cptX <= min || cptX >= max)
        {
            b = !b;
        }
        if (b)
        {
            cptX += 0.2f;
            this.transform.position = new Vector3(this.transform.position.x + 0.2f, this.transform.position.y, this.transform.position.z);
        }
        else
        {
            cptX -= 0.2f;
            this.transform.position = new Vector3(this.transform.position.x - 0.2f, this.transform.position.y, this.transform.position.z);
        }

    }
}
