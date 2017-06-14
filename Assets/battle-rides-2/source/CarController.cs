using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public Rigidbody FrontLeftWheel;
    public Rigidbody FrontRightWheel;

    public float Speed = 10f;

    private float _motorInput;
    //private float _steeringInput;

    public void FixedUpdate() {
        float motor = Speed * Input.GetAxis("Vertical");

        var leftForce = FrontLeftWheel.transform.forward * motor;
        var rightForce = FrontRightWheel.transform.forward * motor;

        FrontLeftWheel.AddForce(FrontLeftWheel.transform.position + leftForce);
        FrontRightWheel.AddForce(FrontRightWheel.transform.position + rightForce);

        DrawDebugLines(leftForce, rightForce);
    }

    private void DrawDebugLines(Vector3 leftForce, Vector3 rightForce) {
        Debug.DrawLine(FrontLeftWheel.transform.position, FrontLeftWheel.transform.position + leftForce.normalized);
        Debug.DrawLine(FrontRightWheel.transform.position, FrontRightWheel.transform.position + rightForce.normalized);

        Debug.DrawLine(FrontLeftWheel.transform.position, FrontLeftWheel.transform.position + FrontLeftWheel.transform.forward, Color.blue);
        Debug.DrawLine(FrontRightWheel.transform.position, FrontRightWheel.transform.position + FrontRightWheel.transform.forward, Color.blue);
    }
}
