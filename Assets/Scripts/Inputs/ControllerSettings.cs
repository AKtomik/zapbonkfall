using UnityEngine;
public enum ControllerMode
{
    TouchOnly, /* Finger touch = Tank rotation + Shoot */
    JoystickAndButton, /* Fixed joystick = Tank rotation | Button = Shoot */
    JoystickShooter, /* Special fixed joystick = Tank rotation + Shoot */
    JoystickShooterDynamic /* Special dynamic joystick  = Tank rotation + Shoot 
                            * Instantiated dynamically on finger position */
}

public enum ShooterMode { SubMachineGun, ChargeGun, None }

public class ControllerSettings : MonoBehaviour
{
    [Header("[CONTROLLER SETTINGS]")]
    [SerializeField] private ControllerMode _controllerMode;

    [Header("[SHOOTER SETTINGS]")]
    [SerializeField] ShooterMode _shooterMode = ShooterMode.SubMachineGun;

    public ControllerMode ControllerMode => _controllerMode; 
    public ShooterMode ShooterMode => _shooterMode;
}
