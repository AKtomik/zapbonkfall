using UnityEngine;

[RequireComponent (typeof(ControllerSettings))]
public class ControllerSetup : MonoBehaviour
{
    private ControllerSettings _settings;
    [SerializeField] private GameObject _touchOnly;
    [SerializeField] private GameObject _joystickAndButton;
    [SerializeField] private GameObject _joystickShooter;
    [SerializeField] private GameObject _joystickShooterDynamic;

    private void Awake()
    {
        _settings = GetComponent<ControllerSettings>(); 

        _touchOnly.SetActive(false);
        _joystickAndButton.SetActive(false);
        _joystickShooter.SetActive(false);
        _joystickShooterDynamic.SetActive(false);

        switch (_settings.ControllerMode)
        {
            case ControllerMode.TouchOnly: _touchOnly.SetActive(true); break;
            case ControllerMode.JoystickAndButton: _joystickAndButton.SetActive(true); break;
            case ControllerMode.JoystickShooter: _joystickShooter.SetActive(true); break;
            case ControllerMode.JoystickShooterDynamic: _joystickShooterDynamic.SetActive(true); break;
        }
    }
}

