using UnityEngine;

public class TouchInputTest : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] Vector3 shiftParticulePoisition;
    Camera cam;

	void Awake() => cam = Camera.main;

    void TapAt(Vector2 screenPos)
    {
        Debug.Log("Tap at: " + screenPos);
        Ray ray = cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
            Instantiate(particle, hit.point + shiftParticulePoisition, transform.rotation);
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    TapAt(touch.position);
                }
            }
        }
    }
}