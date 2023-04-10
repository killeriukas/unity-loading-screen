using TMI.Core;
using UnityEngine;

public class PaddleBehaviour : UnityBehaviour, IUpdatable {

	[SerializeField]
	private Rigidbody2D rigidBody;

	private Vector3 startingPosition;
	
	private IExecutionManager executionManager;

	protected override void Awake() {
		base.Awake();
		startingPosition = transform.position;
	}

	public override void Setup(IInitializer initializer) {
		base.Setup(initializer);
		this.executionManager = initializer.GetManager<ExecutionManager, IExecutionManager>();
	}

	private ExecutionManager.Result OnUpdate() {

		//change this to input later
		if(Input.GetKey(KeyCode.LeftArrow)) {
			rigidBody.velocity = Vector2.left * 20f;
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			rigidBody.velocity = Vector2.right * 20f;
		} else {
			rigidBody.velocity = Vector2.zero;
		}

		return ExecutionManager.Result.Continue;
	}

	public void Initialize() {
		transform.position = startingPosition;
	}

	public void EnableInput() {
		executionManager.Register(this, OnUpdate);
	}

	public void DisableInput() {
		rigidBody.velocity = Vector2.zero;
		executionManager.Unregister(this);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		BallBehaviour ballBehaviour = collision.collider.GetComponentInParent<BallBehaviour>();
		
		//paddle doesn't handle anything but the ball collision
		if(ballBehaviour == null) {
			return;
		}
		
		Vector3 localBallPosition = transform.InverseTransformPoint(ballBehaviour.transform.position);
		Vector3 dir = localBallPosition.normalized;
		ballBehaviour.PushIntoDirection(dir);
	}
		
}