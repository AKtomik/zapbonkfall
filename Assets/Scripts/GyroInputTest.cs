using UnityEngine;
using UnityEngine.InputSystem;

public class GyroInputTest : MonoBehaviour
{
    [SerializeField] InputActionReference attitudeAction;
    [SerializeField] float sensitivity = 1f;
 		Quaternion intialRotation;
    Quaternion lastRotation;

	void Start()
	{
		intialRotation = transform.rotation;
	}

	// device management
	void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        foreach (var d in InputSystem.devices) TryEnable(d);
        attitudeAction.action.Enable();
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
        attitudeAction.action.Disable();
    }

    void OnDeviceChange(InputDevice d, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added) TryEnable(d);
    }

    static void TryEnable(InputDevice d)
    {
        //if (d is Gyroscope || d is AttitudeSensor || d is GravitySensor || d is Accelerometer)
				if (d is AttitudeSensor)
        {
            Debug.Log($"enabling device: {d}");
            InputSystem.EnableDevice(d);
        }
    }

		// script content
		static Quaternion AndroidToUnity(Quaternion q) => Quaternion.Normalize(new Quaternion(-q.x, -q.z, -q.y, q.w));
		
    void Update()
    {
        Quaternion w = attitudeAction.action.ReadValue<Quaternion>();
        if (w == lastRotation) return;
				w = AndroidToUnity(intialRotation * w);
        lastRotation = w;
        transform.rotation = w * intialRotation;
    }
}