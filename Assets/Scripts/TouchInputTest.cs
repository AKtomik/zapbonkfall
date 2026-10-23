using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine;

public class TouchInputTest : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] Vector3 shiftParticulePoisition;

	void OnEnable()
	{
        Debug.Log("Enable TouchInputTest");
		EnhancedTouchSupport.Enable();
	}

	void OnDisable()
	{
        Debug.Log("Disable TouchInputTest");
		EnhancedTouchSupport.Disable();
	}

	void Update()
    {
        foreach(Touch touch in Touch.activeTouches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Tap at : " + touch.screenPosition);

                Ray ray = Camera.main.ScreenPointToRay(touch.screenPosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Instantiate(particle, hit.point + shiftParticulePoisition, transform.rotation);
                }
            }
        }
    }
}