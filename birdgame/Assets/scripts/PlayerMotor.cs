using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 rotationCam = Vector3.zero;

    private Rigidbody rb;

    [SerializeField]
    private Camera cam;

    private GameObject bird;

    [SerializeField]
    private float maxRotY = 45;
    private float maxRotX = 30;
    private float maxRotZ = 10;

    void Start() {
        rb = GetComponent<Rigidbody>();

        bird = cam.transform.Find("bird").gameObject;
    }

    public void Move(float speed) {
        velocity = cam.transform.forward * speed;
    }

    public void Rotate(Vector3 _rotation) {
        rotation = _rotation;
    }

    public void RotateCam(Vector3 _rotationCam) {
        rotationCam = _rotationCam;
    }

    private void FixedUpdate() {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement() {
        if (velocity != Vector3.zero) {
            rb.velocity = velocity * Time.fixedDeltaTime;
        }
    }

    private void PerformRotation() {
        Quaternion newRot = rb.rotation * Quaternion.Euler(0, rotation.y, 0);

        float clampedY = newRot.eulerAngles.y;

        if (newRot.eulerAngles.y > maxRotY && newRot.eulerAngles.y < 360 - maxRotY) {
            //rotation not oke so clamp it
            if (newRot.eulerAngles.y < 180) {
                clampedY = maxRotY;
            } else {
                clampedY = 360 - maxRotY;
            }
        }

        newRot = Quaternion.Euler(0, clampedY, 0);

        rb.MoveRotation(newRot);

        //rotate bird
        //if turning then rotate bird
        float currentRotZ = bird.transform.rotation.eulerAngles.z;
        float currentY = rb.rotation.eulerAngles.y;
        float clampZ = -rotation.y;

        if (currentRotZ + rotation.y > maxRotZ && currentRotZ + rotation.y < 360 - maxRotZ) {
            if (currentRotZ + rotation.y < 180) {
                clampZ -= (currentRotZ + rotation.y) - maxRotZ;
            }
            else {
                clampZ -= (currentRotZ + rotation.y) - (360 - maxRotZ);
            }
        }

        bird.transform.Rotate(0, 0, clampZ);


        //set cam rotation
        float currentRotX = cam.transform.rotation.eulerAngles.x;

        if (currentRotX + rotationCam.x > maxRotX && currentRotX + rotationCam.x < 360 - maxRotX) {
            //is going to far
            if (currentRotX + rotationCam.x < 180) {
                cam.transform.rotation = Quaternion.Euler(maxRotX, 0, 0);
            } else {
                cam.transform.rotation = Quaternion.Euler(360 - maxRotX, 0, 0);
            }
        } else {
            cam.transform.Rotate(rotationCam.x, 0, 0);
        }
    }
}
