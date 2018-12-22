using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 0f;

    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;

    // Start is called before the first frame update
    void Start() {
        motor = GetComponent<PlayerMotor>();
    }

    void Update() {
        //apply movement
        motor.Move(speed);

        //rotation
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRot, 0) * lookSensitivity;
        motor.Rotate(_rotation);

        //rotate camera
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _rotationCam = new Vector3(-_xRot, 0, 0) * lookSensitivity;
        motor.RotateCam(_rotationCam);

    }
}
