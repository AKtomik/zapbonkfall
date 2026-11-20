using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [Header("[REFERENCES]")]
    [SerializeField] private RectTransform _joystickBGrt;   
    [SerializeField] private RectTransform _joystickBtnRt;  
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Transform _tank;

    [Header("[SETTINGS]")]
    [SerializeField] private float _radius = 100f;       
    [SerializeField] private float _rotationSpeed = 6f;  
    [SerializeField] private float _deadZone = 0.1f;     

    private int _currentFingerId = -1;
    private Vector2 _aimDirection;
    private Camera _uiCamera;

    private void Awake()
    {
        _uiCamera = (_canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : _canvas.worldCamera;
        _aimDirection = _tank.up; // évite un saut de rotation au démarrage
    }

    private void Update()
    {
        if (_currentFingerId == -1)
        {            
            foreach (Touch touch in InputManager.GetClickTaps())
            {
                if (IsPointerInJoystickZone(touch.position))
                {
                    _currentFingerId = touch.fingerId;
                    MoveJoystick(touch.position);
                    break;
                }
            }
        }
        else
        {         
            foreach (Touch touch in InputManager.GetClickHolds())
            {
                if (touch.fingerId == _currentFingerId)
                    MoveJoystick(touch.position);
            }

            foreach (Touch touch in InputManager.GetClickLifts())
            {
                if (touch.fingerId == _currentFingerId)
                    Cancel();
            }
        }

        _tank.up = Vector3.RotateTowards(_tank.up, _aimDirection, _rotationSpeed * Time.deltaTime, 0f);
    }

    private bool IsPointerInJoystickZone(Vector2 screenPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(_joystickBGrt, screenPosition, _uiCamera);
    }

    private void MoveJoystick(Vector2 screenPoint)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBGrt, screenPoint, _uiCamera, out Vector2 localPoint);

        Vector2 value = Vector2.ClampMagnitude(localPoint / _radius, 1f); 
        _joystickBtnRt.anchoredPosition = value * _radius;                

        if (value.sqrMagnitude > _deadZone * _deadZone)
            _aimDirection = value;
    }

    private void Cancel()
    {
        _joystickBtnRt.anchoredPosition = Vector2.zero;
        _currentFingerId = -1;
    }

    private void OnDrawGizmosSelected()
    {
        if (_joystickBGrt == null || _canvas == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_joystickBGrt.position, _radius * _canvas.scaleFactor);
    }
}