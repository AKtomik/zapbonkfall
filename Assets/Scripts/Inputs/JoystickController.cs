using UnityEngine;
using UnityEngine.UI; 

public class JoystickController : MonoBehaviour
{
    [Header("[REFERENCES]")]

    // References to move on Joystick Prefab later, only to test with one at beginning
    [SerializeField] private Image _joystickBG;
    [SerializeField] private Image _joystickBtn;


    //private ClickType DetectClickType()
    //{
    //    //foreach (Touch touch in InputManager.GetClickTaps()) { /*CanonRotation(touch.position);*/ }

    //    if (InputManager.GetClickTaps().Length > 0) return ClickType.Tap;
    //    if (InputManager.GetClickHolds().Length > 0) return ClickType.Hold;
    //    if (InputManager.GetClickLifts().Length > 0) return ClickType.Lift;


    //    return ClickType.None;
    //}

    private void Update()
    {
        if (InputManager.GetClickHolds().Length > 0)
        {
            Touch touch = InputManager.GetClickHolds()[0];
            MoveJoystickBtn(touch);
        }
    }

    private void MoveJoystickBtn(Touch touch)
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
        direction = direction.normalized;

        _joystickBtn.rectTransform.anchoredPosition = new Vector2(
            direction.x * (_joystickBG.rectTransform.sizeDelta.x /2),
            direction.y * (_joystickBG.rectTransform.sizeDelta.y /2)) ;

        //_joystickBtn.rectTransform.anchoredPosition = new Vector2(
        //    touch.position.x * (_joystickBG.rectTransform.sizeDelta.x /2), 
        //    touch.position.y * (_joystickBG.rectTransform.sizeDelta.y /2)
        //);
    }

    private void ResetJoystickPosition()
    {
        Debug.Log($"Release touch screen"); 

        _joystickBtn.rectTransform.anchoredPosition = Vector2.zero;
    }
}
