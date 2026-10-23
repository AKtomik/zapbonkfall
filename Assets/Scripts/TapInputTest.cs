using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInputTest : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] Vector3 shiftParticulePoisition;
    [SerializeField] InputActionReference tapAction;
    [SerializeField] InputActionReference pointAction;

    Camera cam;

	void Awake() => cam = Camera.main;

	void OnEnable()
	{
        Debug.Log("Enable TouchInputTest");
		//EnhancedTouchSupport.Enable();
        tapAction.action.performed += OnTap;
        tapAction.action.Enable();
        pointAction.action.Enable();
	}

	void OnDisable()
	{
        Debug.Log("Disable TouchInputTest");
		//EnhancedTouchSupport.Disable();
        tapAction.action.performed -= OnTap;
        tapAction.action.Disable();
        pointAction.action.Disable();
	}

    void OnTap(InputAction.CallbackContext ctx)
    {
        Vector2 screenPos = pointAction.action.ReadValue<Vector2>();
        Debug.Log("Tap at: " + screenPos);
        Ray ray = cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
            Instantiate(particle, hit.point + shiftParticulePoisition, transform.rotation);
    }

	void Update()
    {
        //foreach(Touch touch in Touch.activeTouches)
        //{
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Debug.Log("Tap at : " + touch.screenPosition);

        //        Ray ray = Camera.main.ScreenPointToRay(touch.screenPosition);
        //        if (Physics.Raycast(ray, out RaycastHit hit))
        //        {
        //            Instantiate(particle, hit.point + shiftParticulePoisition, transform.rotation);
        //        }
        //    }
        //}
    }
}