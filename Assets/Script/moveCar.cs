using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCar : MonoBehaviour {

    public float moveSpeed = 0.5f;
    public float rotateSpeed = 30f;

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(0, 0, 3f, ForceMode.Force);


        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
            rb.AddRelativeForce(0, moveSpeed*2/3, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime, Space.World);
            rb.AddRelativeForce(0, moveSpeed * 2 / 3, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
            rb.AddRelativeForce(0, -moveSpeed/2, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime, Space.World);
            rb.AddRelativeForce(0, -moveSpeed/2, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddRelativeForce(0, -moveSpeed/2, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddRelativeForce(0, moveSpeed, 0, ForceMode.Acceleration);
        }
        /*if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(0, 0, 0.2f, ForceMode.Impulse);
        }*/
    }
}
