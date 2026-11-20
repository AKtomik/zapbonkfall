using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [Header("[REFERENCES]")]
    [SerializeField] private RectTransform _joystickBGrt;   
    [SerializeField] private RectTransform _joystickBtnRt;
    [SerializeField] private ShootingController _shooter; 

    [Header("[SETTINGS]")]
    [SerializeField] private float _radius = 172f;
    [SerializeField] private float _deadZone = 0.1f;
    [SerializeField] private bool _shootWithJoystick;
    [SerializeField] private bool _isDynamic;

    private Canvas _canvas;
    private Camera _uiCamera;
    private int _currentFingerId = -1;

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        if (_canvas == null) { Debug.LogError($" Canvas not auto assigned on {this.transform.parent.gameObject.name}");  }

        _uiCamera = (_canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : _canvas.worldCamera;
    }

    private void OnEnable() /* Dynamic joystick is hidden when screen isn't touched */
    {
        _joystickBGrt.gameObject.SetActive(!_isDynamic); 
    }

    private void Update()
    {
        if (_currentFingerId == -1)
        {            
            foreach (Touch touch in InputManager.GetClickTaps())
            {
                if (!IsPointerInJoystickZone(touch.position)) continue; 
                else
                {
                    GrabJoystick(touch);
                    break; 
                }
            }
        }
        else
        {         
            foreach (Touch touch in InputManager.GetClickHolds())
            {
                if (touch.fingerId != _currentFingerId) continue; 
                else
                {
                    MoveJoystick(touch.position);
                }
                if (_shootWithJoystick) _shooter.ClickHandler(ClickType.Hold);

            }

            foreach (Touch touch in InputManager.GetClickLifts())
            {
                if (touch.fingerId == _currentFingerId) { ReleaseJoystick();}
            }
        }

    }

    private bool IsPointerInJoystickZone(Vector2 screenPosition)
    {
        return _isDynamic || RectTransformUtility.RectangleContainsScreenPoint(_joystickBGrt, screenPosition, _uiCamera);
    }

    private void GrabJoystick(Touch touch)
    {
        _currentFingerId = touch.fingerId;

        if (_isDynamic)
        {
            _joystickBGrt.position = touch.position; 
            _joystickBGrt.gameObject.SetActive(true);
        }

        MoveJoystick(touch.position);
        if (_shootWithJoystick) _shooter.ClickHandler(ClickType.Tap);
    }

    private void MoveJoystick(Vector2 screenPoint)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBGrt, screenPoint, _uiCamera, out Vector2 localPoint);

        Vector2 value = Vector2.ClampMagnitude(localPoint / _radius, 1f); 
        _joystickBtnRt.anchoredPosition = value * _radius;

        if (value.sqrMagnitude > _deadZone * _deadZone) { _shooter.SetDirection(value); }
    }

    private void ReleaseJoystick()
    {
        _joystickBtnRt.anchoredPosition = Vector2.zero;
        _currentFingerId = -1;

        if (_isDynamic) _joystickBGrt.gameObject.SetActive(false);
        if (_shootWithJoystick) _shooter.ClickHandler(ClickType.Lift);
    }

    // --- Tools --- //
    private void OnDrawGizmosSelected()
    {
        if (_joystickBGrt == null || _canvas == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_joystickBGrt.position, _radius * _canvas.scaleFactor);
    }
}