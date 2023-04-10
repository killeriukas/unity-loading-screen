using TMI.Core;
using TMI.Helper;
using TMI.State;

public class GameManager : BaseNotificationManager, IGameManager {

	private GameController gameController;
	
	private IStateMachine stateMachine;

	public void Initialize(GameplayItems gameplayItems) {
		gameController = new GameController(initializer);
		gameplayItems.gameController = gameController;
		
		stateMachine = StateMachine.Create(new LoadGameAssetsState(initializer, gameplayItems));
		RegisterUpdate();
	}

	protected override ExecutionManager.Result OnUpdate() {
		stateMachine.Update();
		return ExecutionManager.Result.Continue;
	}

	public override void OnPreDestroy() {
		UnregisterUpdate();
		GeneralHelper.DisposeAndMakeDefault(ref gameController);
		GeneralHelper.DisposeAndMakeDefault(ref stateMachine);
		base.OnPreDestroy();
	}

}
