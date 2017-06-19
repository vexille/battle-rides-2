using UnityEngine;

public class CarController : MonoBehaviour {

    public CarBalanceData CarData;

    public Transform Steering;
    public Rigidbody FrontLeftWheel;
    public Rigidbody FrontRightWheel;

    private float _motorInput;
    private float _steeringInput;
    private Rigidbody _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        _motorInput = CarData.Speed * 100f * Input.GetAxis("Vertical");
        _steeringInput = CarData.MaxTurningAngle * Input.GetAxis("Horizontal");

        Steering.localRotation = Quaternion.Euler(0f, _steeringInput, 0f);

		var steeringDirection = Steering.forward;
		var leftForce = steeringDirection * _motorInput;
		var rightForce = steeringDirection * _motorInput;
		//var mainForce = Steering.forward * _motorInput;

		FrontLeftWheel.AddForce(leftForce);
		FrontRightWheel.AddForce(rightForce);
		//_rigidbody.AddForce(mainForce);

		UpdateFriction();
    }

    public void FixedUpdate() {
        

       // DrawDebugLines(leftForce, rightForce);
    }

    private void UpdateFriction() {
        Vector3 impulse = _rigidbody.mass * -GetLateralVelocity();
        _rigidbody.AddForce(impulse, ForceMode.Impulse);
    }

    private Vector3 GetLateralVelocity() {
        var rightNormal = transform.right;
        return Vector3.Dot(rightNormal, _rigidbody.velocity) * rightNormal;
    }

    private void DrawDebugLines(Vector3 leftForce, Vector3 rightForce) {
        Debug.DrawLine(FrontLeftWheel.transform.position, FrontLeftWheel.transform.position + leftForce.normalized);
        Debug.DrawLine(FrontRightWheel.transform.position, FrontRightWheel.transform.position + rightForce.normalized);

        Debug.DrawLine(FrontLeftWheel.transform.position, FrontLeftWheel.transform.position + FrontLeftWheel.transform.forward, Color.blue);
        Debug.DrawLine(FrontRightWheel.transform.position, FrontRightWheel.transform.position + FrontRightWheel.transform.forward, Color.blue);

        //var mainVelocity = _rigidbody.velocity;
        //Debug.DrawLine(transform.position, transform.position + mainVelocity, Color.red);
    }

    private void OnGUI() {
        var mainVelocity = _rigidbody.velocity;
        //var leftVelocity = FrontLeftWheel.velocity;
        //var rightVelocity = FrontRightWheel.velocity;
        GUIStyle style = new GUIStyle();
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.yellow;

        GUILayout.Label(string.Format("Main speed: {0:0.000} ({1:0.#}, {2:0.#}, {3:0.#})", mainVelocity.magnitude, mainVelocity.x, mainVelocity.y, mainVelocity.z), style);
        //GUILayout.Label(string.Format("LWheel speed: {0:0.000} ({1:0.#}, {2:0.#}, {3:0.#})", leftVelocity.magnitude, leftVelocity.x, leftVelocity.y, leftVelocity.z), style);
        //GUILayout.Label(string.Format("RWheel speed: {0:0.000} ({1:0.#}, {2:0.#}, {3:0.#})", rightVelocity.magnitude, rightVelocity.x, rightVelocity.y, rightVelocity.z), style);
    }
}
