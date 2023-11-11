using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    public Vector3 gravityAcceleration;

    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(gravityAcceleration);
    }
}
