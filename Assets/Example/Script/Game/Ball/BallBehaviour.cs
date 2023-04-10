using TMI.Core;
using TMI.Notification;
using UnityEngine;

public class BallBehaviour : BaseNotificationBehaviour, IUpdatable {

	[SerializeField]
	private Rigidbody2D rigidBody;

	[SerializeField]
	private Transform deathZoneTransform;

	private Vector3 startingPosition;

	protected override void Awake() {
		base.Awake();
		startingPosition = transform.position;
	}

	private const float SPEED = 30f;
	
	public void Initialize(PaddleBehaviour paddle) {
		transform.position = startingPosition;
		rigidBody.isKinematic = true;
		transform.SetParent(paddle.transform, true);
	}

	public void KickOff() {
		transform.SetParent(null);
		rigidBody.isKinematic = false;
		
		//Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
		
		rigidBody.velocity = Vector2.up * SPEED;
	}

	public void PushIntoDirection(Vector2 direction) {
		rigidBody.velocity = direction * SPEED;
	}

	public void Freeze() {
		rigidBody.isKinematic = true;
		rigidBody.velocity = Vector2.zero;
		rigidBody.position = deathZoneTransform.position;
	}
	
	public void Kill() {
		Freeze();
		Trigger(new BallKilledNotification());
	}
}