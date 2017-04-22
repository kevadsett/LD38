// extended from https://gist.github.com/tiagosr/43b06604c53ccb327083
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class MixableCamera : MonoBehaviour {

	public Camera FromCamera, ToCamera;

	public float mixSpeed = 10f;

	private float _mixPosition = 0f;

	private Camera _camera;

	void OnEnable()
	{
		_camera = GetComponent<Camera> ();
	}

	void OnPreCull() {
		// OnPreCull is only called for components attached to cameras
		if((FromCamera != null) && (ToCamera != null)) {
			transform.position = Vector3.Lerp(FromCamera.transform.position, ToCamera.transform.position, _mixPosition);
			transform.rotation = Quaternion.Lerp(FromCamera.transform.rotation, ToCamera.transform.rotation, _mixPosition);
			//transform.localScale = Vector3.Lerp(fromCamera.transform.localScale, toCamera.transform.localScale, mixPosition);
			_camera.farClipPlane = Mathf.Lerp(FromCamera.farClipPlane, ToCamera.farClipPlane, _mixPosition);
			_camera.nearClipPlane = Mathf.Lerp(FromCamera.nearClipPlane, ToCamera.nearClipPlane, _mixPosition);
			if(ToCamera.orthographic) {
				_camera.orthographicSize = Mathf.Lerp(FromCamera.orthographicSize, ToCamera.orthographicSize, _mixPosition);
			} else {
				_camera.fieldOfView = Mathf.Lerp(FromCamera.fieldOfView, ToCamera.fieldOfView, _mixPosition);
			}
			_camera.rect.Set (
				Mathf.Lerp (FromCamera.rect.x, ToCamera.rect.x, _mixPosition),
				Mathf.Lerp (FromCamera.rect.y, ToCamera.rect.y, _mixPosition),
				Mathf.Lerp (FromCamera.rect.width, ToCamera.rect.width, _mixPosition),
				Mathf.Lerp (FromCamera.rect.height, ToCamera.rect.height, _mixPosition)
			);
		} else if(FromCamera != null) {
			transform.position = FromCamera.transform.position;
			transform.rotation = FromCamera.transform.rotation;
			_camera.CopyFrom(FromCamera);
		} else if(ToCamera != null) {
			transform.position = ToCamera.transform.position;
			transform.rotation = ToCamera.transform.rotation;
			_camera.CopyFrom(ToCamera);
		}

		_mixPosition = Mathf.Clamp(_mixPosition + mixSpeed * Time.deltaTime, 0f, 1f);
	}
	public void Reset()
	{
		_mixPosition = 0.0f;
	}
}
