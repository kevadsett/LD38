using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
	public GameObject PlayerObject;

	[RangeAttribute (0f, 1f)]
	public float QueueWeighting;

	public float ZoomPerFriend;
	public float ZoomSpeed;

	private Vector3 _currentVelocity;
	private Camera _camera;
	private float _defaultZoom;

	void Awake()
	{
		_camera = GetComponent<Camera>();
		_defaultZoom = _camera.orthographicSize;
	}

	void LateUpdate ()
	{
		Vector3 playerPos = PlayerObject.transform.position;
		Vector3 queuePos = OrderlyQueueficator.GetAveragePosition ();
		Vector3 camPos = Vector3.Lerp (playerPos, queuePos, QueueWeighting);

		transform.position = new Vector3 (camPos.x, camPos.y, transform.position.z);

		float targetSize = _defaultZoom + ZoomPerFriend * OrderlyQueueficator.GetLength ();
		float size = _camera.orthographicSize;

		if (size < targetSize) {
			size = Mathf.Min (targetSize, size + ZoomSpeed * Time.deltaTime);
		} else if (size > targetSize) {
			size = Mathf.Max (targetSize, size - ZoomSpeed * Time.deltaTime);
		}

		_camera.orthographicSize = size;
	}
}
