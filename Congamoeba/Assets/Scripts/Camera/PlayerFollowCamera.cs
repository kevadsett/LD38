using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
	public GameObject PlayerObject;

	private Vector3 _currentVelocity;

	void LateUpdate ()
	{
		transform.position = new Vector3 (PlayerObject.transform.position.x, PlayerObject.transform.position.y, transform.position.z);
	}
}
