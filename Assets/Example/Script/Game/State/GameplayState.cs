using TMI.Core;
using TMI.State;

public class GameplayState : BaseStateWithProxy<GameplayItems> {

    public GameplayState(IInitializer initializer, GameplayItems gameplayItems) : base(initializer, gameplayItems) {
        
    }

    public override void Enter() {
        base.Enter();

        Listen<BallKilledNotification>(this, OnBallKilled);
        Listen<BrickDestroyedNotification>(this, OnBrickDestroyed);
    }
    private void OnBrickDestroyed(BrickDestroyedNotification notification) {
        proxy.gameController.RemoveBrick();

        if(!proxy.gameController.anyBricksLeft) {
            proxy.ballBehaviour.Freeze();
            proxy.paddleBehaviour.DisableInput();
            nextState = new GameWonState(initializer, proxy);
        }
    }
    
    private void OnBallKilled(BallKilledNotification notification) {
        proxy.gameController.RemoveLife();
        proxy.paddleBehaviour.DisableInput();

        if(proxy.gameController.isAlive) {
            nextState = new WaitBallKickOffState(initializer, proxy);
        } else {
            nextState = new GameOverState(initializer, proxy);
        }
    }

    public override void Exit() {
        StopListen<BallKilledNotification>(this);
        StopListen<BrickDestroyedNotification>(this);
        base.Exit();
    }

}