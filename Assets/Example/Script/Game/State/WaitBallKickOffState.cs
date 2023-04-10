using TMI.Core;
using TMI.State;
using UnityEngine;

public class WaitBallKickOffState : BaseStateWithProxy<GameplayItems> {

    public WaitBallKickOffState(IInitializer initializer, GameplayItems gameplayItems) : base(initializer, gameplayItems) {
        
    }

    public override void Enter() {
        base.Enter();
        
        proxy.paddleBehaviour.Initialize();
        proxy.ballBehaviour.Initialize(proxy.paddleBehaviour);
        
        proxy.paddleBehaviour.EnableInput();
    }

    public override void Update() {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space)) {
            proxy.ballBehaviour.KickOff();
            nextState = new GameplayState(initializer, proxy);
        }
        
    }

}