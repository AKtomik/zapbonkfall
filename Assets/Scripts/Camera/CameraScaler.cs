using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour {

    private int currentWidth = 0;
    private int currentHeight = 0;

    [SerializeField] float cameraWidthUnits = 33.5f;
	[SerializeField] float cameraHeightUnits = 20f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        SetRes();
    }

    private void SetRes()
    {
        currentWidth = Screen.width;
        currentHeight = Screen.height;
        float width_size = (float)(cameraWidthUnits * Screen.height / Screen.width * 0.5);
        float height_size = (float)(cameraHeightUnits * Screen.width / Screen.height * 0.5) * ((float)Screen.height/ Screen.width);
        cam.orthographicSize = Mathf.Max(height_size, width_size);
        Debug.Log($"cam.orthographicSize changed to {cam.orthographicSize}");
    }

    void Update()
    {
        if(Screen.width != currentWidth || Screen.height != currentHeight)
        {
            SetRes();
        }
    }
}
