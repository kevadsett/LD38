using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
	public GameObject PlayerObject;

	[RangeAttribute (0f, 1f)]
	public float QueueWeighting;

	private Vector3 _currentVelocity;

	private Camera _camera;

	void Awake()
	{
		_camera = GetComponent<Camera>();
	}

	void LateUpdate ()
	{
		Vector3 playerPos = PlayerObject.transform.position;
		Vector3 queuePos = OrderlyQueueficator.GetAveragePosition ();
		Vector3 camPos = Vector3.Lerp (playerPos, queuePos, QueueWeighting);

		transform.position = new Vector3 (camPos.x, camPos.y, transform.position.z);

		_camera.orthographicSize = 4 * PlayerObject.transform.localScale.x;
	}
}
