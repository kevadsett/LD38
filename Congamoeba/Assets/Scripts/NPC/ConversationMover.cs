using UnityEngine;
using System.Collections;

namespace Congamoeba.NPC
{
	public class ConversationMover : MonoBehaviour {
		public Vector2 ConversationPosition;

		private Vector2 _originalPosition;

		public AnimationCurve Transition;

		public float MixSpeed;

		private float _mixPosition;

		private Vector2 _targetPosition;

		private bool _isEnabled;

		void Start ()
		{
			_originalPosition = transform.position;
			_targetPosition = _originalPosition;
		}

		void Update ()
		{
			if (_isEnabled == false && _mixPosition >= 1f)
			{
				return;
			}
			_mixPosition = Mathf.Clamp(_mixPosition + MixSpeed * Time.deltaTime, 0f, 1f);
			float lerpPosition = Transition.Evaluate (_mixPosition);

			transform.position = Vector3.Lerp (transform.position, new Vector3 (_targetPosition.x, _targetPosition.y, transform.position.z), lerpPosition);
		}

		public void Enable()
		{
			_isEnabled = true;
		}

		public void Disable()
		{
			_isEnabled = false;
		}

		public void MoveIntoConversation(Transform playerTransform)
		{
			_targetPosition = new Vector2(
				(playerTransform.position.x + ConversationPosition.x),
				(playerTransform.position.y + ConversationPosition.y)
			);
			_mixPosition = 0;
		}

		public void MoveOutOfConversation()
		{
			_targetPosition = _originalPosition;
			_mixPosition = 0;
		}
	}
}
