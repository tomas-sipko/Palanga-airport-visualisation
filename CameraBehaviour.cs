using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Transform target;
    public float distance = 20.0f;
    public float zoomSpd = 2.0f;

    public float xSpeed = 240.0f;
    public float ySpeed = 123.0f;

    public int yMinLimit = -723;
    public int yMaxLimit = 877;

    public float x = 22.0f;
    public float y = 33.0f;

    public float plusHeight = 20;
    private Vector3 initialState;
    private Quaternion initialRotation;

    public void Start()
    {
        initialState = transform.position;
        initialRotation = transform.rotation;

        Rigidbody comp = GetComponent<Rigidbody>();
        if (comp)
            comp.freezeRotation = true;
    }

    public void Reset()
    {
        transform.position = initialState;
        transform.rotation = initialRotation;

        target = null;
    }

    public void LateUpdate()
    {
        if (target)
        {
            x -= Input.GetAxis("Horizontal") * xSpeed * 0.02f;
            y += Input.GetAxis("Vertical") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            distance -= Input.GetAxis("Fire1") * zoomSpd * 0.02f;
            distance += Input.GetAxis("Fire2") * zoomSpd * 0.02f;

            Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
            Vector3 position = rotation * new Vector3(distance, 0.0f, 0.0f) + target.position + new Vector3(0, plusHeight, 0);
            //transform.rotation = rotation;
            transform.position = position;

            if (Time.frameCount % 30 == 0)
            {
                transform.LookAt(target.position);
            }
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0f)
            angle += 360.0f;
        if (angle > 360.0f)
            angle -= 360.0f;
        return angle;
    }
}
