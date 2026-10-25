using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class GyroInputTest : MonoBehaviour
{
    [SerializeField] InputActionReference attitudeAction; // Vector3
    [SerializeField] float sensitivity = 1f;

    void OnEnable()
    {
        Debug.Log($"SystemInfo.supportsGyroscope = {SystemInfo.supportsGyroscope}\nGyroscope.current = {Gyroscope.current}");
        Debug.Log($"AttitudeSensor.current = {AttitudeSensor.current}");
        Debug.Log($"GravitySensor.current = {GravitySensor.current}");
        Debug.Log($"Accelerometer.current = {Accelerometer.current}");
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
        if (d is Gyroscope || d is AttitudeSensor || d is GravitySensor || d is Accelerometer)
        {
            Debug.Log($"enabling device: {d}");
            InputSystem.EnableDevice(d);
        }
    }

    Quaternion lastw;
    void Update()
    {
        Quaternion w = attitudeAction.action.ReadValue<Quaternion>(); // zero until the device exists
        if (w == lastw) return;
        Debug.Log($"change angle: {w}");
        lastw = w;
        transform.rotation = w;
    }
}