using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }


    public void Move(Vector3 inputV) {
        velocity = inputV;
    }

    public void Rotate(Vector3 _rotation) {
        rotation = _rotation;
    }

    private void FixedUpdate() {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement() {
        if (velocity != Vector3.zero) {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation() {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }
}
