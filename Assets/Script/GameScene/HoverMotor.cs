using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMotor : MonoBehaviour {

    public GameObject gameMaster;

    public float speed = 10f;
    public float turnSpeed = 0.05f;
    public float hoverForce = 2f;
    public float hoverHeight = 0.75f;

    private float powerInput;
    private float turnInput;
    private float resetInput;
    private Rigidbody carRigidbody;

    void Awake()
    {
        carRigidbody = GetComponent <Rigidbody>();
    }

    void Update()
    {
        if (carRigidbody.transform.position.y <= 0)
        {
            gameMaster.GetComponent<GameManager>().ResetLastCp();
        }


        powerInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            carRigidbody.AddForce(appliedHoverForce, ForceMode.Impulse);
        }

        carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
        carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
    }
}
