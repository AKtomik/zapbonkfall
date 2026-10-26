using System.Linq;
using UnityEngine;

// class used to interact with Input and others systems to translate inputs into actions
// so that we can have multiples inputs for same action in the future (and that just cleaner) 
// this class is fully encapsulating Input
public static class InputManager
{
	// TAP / CLICK
	private static Touch[] AllTouchs()
	{
		return Input.touches;
		// simulate click too later
	}

	public static Touch[] GetClickTaps()
	{
		return AllTouchs().Where(touch => touch.phase == TouchPhase.Began).ToArray();
	}

	public static Touch GetClickOneTap()
	{
		return GetClickTaps()[0];
	}
	
	public static Touch[] GetClickHolds()
	{
		return AllTouchs().Where(touch => touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved).ToArray();
	}
	
	public static Touch[] GetClickLifts()
	{
		return AllTouchs().Where(touch => touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled).ToArray();
	}

	// GYROSCOPE

	// enable gyro
	public static void EnableGyro()
	{
		Input.gyro.enabled = true;
	}

	public static void DisableGyro()
	{
		Input.gyro.enabled = false;
	}

	// read gyro
	private static Quaternion AndroidToUnity(Quaternion q) => Quaternion.Normalize(new Quaternion(-q.x, -q.z, -q.y, q.w));
	public static Quaternion ReadGyroAttitudeRotation()
	{
		return AndroidToUnity(Input.gyro.attitude);
	}
	
	public static float ReadGyroAccelerationMagnitude()
	{
		return Input.acceleration.magnitude;
	}
	
	// vector magnitude <= 1 (can be less)
	public static Vector2 ReadMovement2D()
	{
		Vector2 dir = Vector3.zero;
		
		// acceleration from gyro
		dir += new Vector2(-Input.acceleration.y, Input.acceleration.x);

		// debug using keyboard
		dir += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		// clamp acceleration vector to unit sphere
		if (dir.sqrMagnitude > 1) dir.Normalize();
		return dir;
	}
}