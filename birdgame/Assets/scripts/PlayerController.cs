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
        //calc movemenent
        Vector3 moveVertical = transform.forward;

        //final movement
        Vector3 velocity = transform.forward.normalized * speed;

        //apply movement
        motor.Move(velocity);

        //rotation
        float _yRot = Input.GetAxisRaw("Mouse X");
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _rotation = new Vector3(_xRot, _yRot, 0) * lookSensitivity;

        motor.Rotate(_rotation);
    }
}
