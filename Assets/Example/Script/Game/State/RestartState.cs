using Example.UI.UIToolkit;
using TMI.Core;
using TMI.State;
using TMI.UI;
using UnityEngine;
using UIManager = TMI.UI.UIToolkit.UIManager;

public class RestartState : BaseStateWithProxy<GameplayItems> {

    private IUIManager uiManager;
    
    public RestartState(IInitializer initializer, GameplayItems proxy) : base(initializer, proxy) {
        uiManager = initializer.GetManager<UIManager, IUIManager>();
    }

    public override void Enter() {
        base.Enter();
        
        for(int i = 0; i < proxy.brickContainerTransform.childCount; i++) {
            GameObject.Destroy(proxy.brickContainerTransform.GetChild(i).gameObject);
        }
        
        proxy.gameController.Dispose();
        proxy.gameController = new GameController(initializer);
        
        GameUIController gameController = uiManager.Load<GameUIController>(false);
        gameController.Initialize(proxy.gameController);

        nextState = new SpawnBricksState(initializer, proxy);
    }
}
