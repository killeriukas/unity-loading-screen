using UnityEngine;

public class DeathWallBehaviour : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col) {
        BallBehaviour ballBehaviour = col.gameObject.GetComponentInParent<BallBehaviour>();
        
        //paddle doesn't handle anything but the ball collision
        if(ballBehaviour == null) {
            return;
        }
        
        ballBehaviour.Kill();
    }

}