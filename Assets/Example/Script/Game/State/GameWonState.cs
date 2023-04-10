using Example.UI.UIToolkit;
using TMI.Core;
using TMI.State;
using TMI.UI;
using UIManager = TMI.UI.UIToolkit.UIManager;

public class GameWonState : BaseStateWithProxy<GameplayItems> {

    private IUIManager uiManager;

    public GameWonState(IInitializer initializer, GameplayItems proxy) : base(initializer, proxy) {
        uiManager = initializer.GetManager<UIManager, IUIManager>();
    }

    public override void Enter() {
        base.Enter();

        GameUIController gameController = uiManager.Load<GameUIController>();
        gameController.ShowGameWonScreen(proxy.gameController.currentScore);

        gameController.onContinueClicked += OnContinue;
    }

    private void OnContinue() {
        nextState = new SpawnBricksState(initializer, proxy);
    }

    public override void Exit() {
        GameUIController gameController = uiManager.Load<GameUIController>(false);
        
        gameController.onContinueClicked -= OnContinue;
        gameController.CloseGameWonScreen();
        
        base.Exit();
    }

}
