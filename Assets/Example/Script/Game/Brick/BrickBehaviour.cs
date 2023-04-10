using TMI.Notification;
using UnityEngine;

public class BrickBehaviour : BaseNotificationBehaviour {

	//you can spawn whatever you want on the hit if necessary

	private bool isDestroyed = false;
	
	private void OnCollisionEnter2D(Collision2D collision) {

		//ignore the double collision
		if(isDestroyed) {
			return;
		}

		isDestroyed = true;
		
		Trigger(new BrickDestroyedNotification());
		
		Destroy(gameObject);
	}

}
