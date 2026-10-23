using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInputTest : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] Vector3 deltaFallPoisition;

    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    // Create a particle if hit
                    Instantiate(particle, hit.point + deltaFallPoisition, transform.rotation);
                }
            }
        }
    }
}