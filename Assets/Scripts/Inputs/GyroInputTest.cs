using UnityEngine;

public class GyroInputTest : MonoBehaviour
{
	void OnEnable()
	{
		InputManager.EnableGyro();
	}

	void OnDisable()
	{
		InputManager.DisableGyro();	
	}

	void Update()
	{
		transform.rotation = InputManager.ReadGyroAttitudeRotation();
	}
}