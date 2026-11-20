using UnityEngine;

public class ShootBtnContoller : MonoBehaviour
{
    [SerializeField] private ShootingController _shooter;
    private RectTransform _rect;
    private int _currentFingerId = -1;

    private void Awake() => _rect = GetComponent<RectTransform>();

    private void Update()
    {
        if (_currentFingerId == -1)
        {
            foreach (Touch touch in InputManager.GetClickTaps())
            {
                if (!RectTransformUtility.RectangleContainsScreenPoint(_rect, touch.position, null)) continue;
                _currentFingerId = touch.fingerId;
                _shooter.ClickHandler(ClickType.Tap);
                break;
            }
        }
        else
        {
            foreach (Touch touch in InputManager.GetClickHolds())
            {
                if (touch.fingerId == _currentFingerId) _shooter.ClickHandler(ClickType.Hold);
            }

            foreach (Touch touch in InputManager.GetClickLifts())
            {
                if (touch.fingerId != _currentFingerId) continue;
                _shooter.ClickHandler(ClickType.Lift);
                _currentFingerId = -1;
            }
        }
    }
}
