using UnityEngine;

public class TouchOnlyController : MonoBehaviour
{
    [SerializeField] private ShootingController _shooter;
    private Camera _camera;

    private void Awake() => _camera = Camera.main;

    private void Update()
    {
        Touch[] taps = InputManager.GetClickTaps();
        if (taps.Length > 0)
        {
            AimAt(taps[0].position);
            _shooter.ClickHandler(ClickType.Tap);
            return;
        }

        Touch[] holds = InputManager.GetClickHolds();
        if (holds.Length > 0)
        {
            AimAt(holds[0].position);
            _shooter.ClickHandler(ClickType.Hold);
            return;
        }

        if (InputManager.GetClickLifts().Length > 0)
            _shooter.ClickHandler(ClickType.Lift);
    }

    private void AimAt(Vector2 screenPos)
    {
        Vector2 direction = _camera.ScreenToWorldPoint(screenPos) - _shooter.transform.position;
        _shooter.SetDirection(direction);
    }
}

