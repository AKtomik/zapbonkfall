using UnityEngine;

public class GyroInputTest : MonoBehaviour
{
	void Start()
	{
		Input.gyro.enabled = true;
	}

	static Quaternion AndroidToUnity(Quaternion q) => Quaternion.Normalize(new Quaternion(-q.x, -q.z, -q.y, q.w));

	void Update()
	{
		Quaternion rotation = AndroidToUnity(Input.gyro.attitude);
		transform.rotation = rotation;
	}
}