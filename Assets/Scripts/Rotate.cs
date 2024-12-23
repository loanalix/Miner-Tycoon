using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float currentSpeed = 0.0f;
    public float speed = 1f;
    public Vector3 rotationAxis = Vector3.up;

    void Start()
    {
        StartRotation();
    }

    void FixedUpdate()
    {
        transform.Rotate(rotationAxis * currentSpeed);
    }

    public void StartRotation()
    {
        currentSpeed = speed;
    }
    public void StopRotation()
    {
        currentSpeed = 0.0f;
    }
}
