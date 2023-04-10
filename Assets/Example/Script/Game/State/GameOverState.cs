using Example.UI.UIToolkit;
using TMI.Core;
using TMI.SceneManagement;
using TMI.State;
using TMI.UI;
using UIManager = TMI.UI.UIToolkit.UIManager;

public class GameOverState : BaseStateWithProxy<GameplayItems> {

    private IUIManager uiManager;
    private ISceneManager sceneManager;
    
    public GameOverState(IInitializer initializer, GameplayItems proxy) : base(initializer, proxy) {
        uiManager = initializer.GetManager<UIManager, IUIManager>();
        sceneManager = initializer.GetManager<SceneManager, ISceneManager>();
    }

    public override void Enter() {
        base.Enter();

        GameUIController gameController = uiManager.Load<GameUIController>();
        gameController.ShowGameOverScreen(proxy.gameController.currentScore);

        gameController.onRestartClicked += OnRestart;
        gameController.onQuitClicked += OnQuit;
    }

    private void OnRestart() {
        nextState = new RestartState(initializer, proxy);
    }

    private void OnQuit() {
        sceneManager.LoadAsync(SceneConstant.main_menu);
    }

    public override void Exit() {
        GameUIController gameController = uiManager.Load<GameUIController>(false);
        
        gameController.onRestartClicked -= OnRestart;
        gameController.onQuitClicked -= OnQuit;
        gameController.CloseGameOverScreen();
        
        base.Exit();
    }

}
