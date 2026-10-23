using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class GyroInputTest : MonoBehaviour
{
    [SerializeField] InputActionReference gyroAction; // Vector3
    [SerializeField] float sensitivity = 1f;

    void OnEnable()
    {
        Debug.Log($"SystemInfo.supportsGyroscope = {SystemInfo.supportsGyroscope}");
        Debug.Log($"Gyroscope.current = {Gyroscope.current}");
        InputSystem.onDeviceChange += OnDeviceChange;
        foreach (var d in InputSystem.devices) TryEnable(d);
        gyroAction.action.Enable();
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
        gyroAction.action.Disable();
    }

    void OnDeviceChange(InputDevice d, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added) TryEnable(d);
    }

    static void TryEnable(InputDevice d)
    {
        if (d is Gyroscope || d is AttitudeSensor || d is GravitySensor)
            InputSystem.EnableDevice(d);
    }

    Vector3 lastw;
    void Update()
    {
        Vector3 w = gyroAction.action.ReadValue<Vector3>(); // zero until the device exists
        if (w == lastw) return;
        lastw = w;
        Debug.Log("w change to " + w);
        transform.Rotate(-w * (Mathf.Rad2Deg * Time.deltaTime * sensitivity), Space.Self);
    }
}