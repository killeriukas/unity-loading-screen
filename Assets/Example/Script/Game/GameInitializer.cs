using TMI.Core;
using TMI.AssetManagement;
using TMI.ConfigManagement.Unity.UI;
using TMI.Core.Unity;
using UnityEngine;
using TMI.Helper;

public class GameInitializer : Initializer {

    [SerializeField]
    private PaddleBehaviour paddleBehaviour;

    [SerializeField]
    private BallBehaviour ballBehaviour;

    [SerializeField]
    private BrickBehaviour brickPrefab;

    [SerializeField]
    private Transform brickContainerTransform;

    private IGameManager gameManager;

	protected override IGroup CreateUIAssetCacheGroup() {
        IUIConfig uiConfig = uiManagerDefault.GetConfig();
        
        UIConfigGroup assetGroup = new UIConfigGroup(uiConfig);
        assetGroup.Add("game_screen");
        return assetGroup;
	}

    protected override void RegisterManagers(IAcquirer acquirer) {
        base.RegisterManagers(acquirer);
        gameManager = acquirer.AcquireManager<GameManager, IGameManager>();
    }

    protected override void OnUIAssetsCached() {
        paddleBehaviour.Setup(this);
        ballBehaviour.Setup(this);
        
        GameplayItems items = new GameplayItems() {
            ballBehaviour = this.ballBehaviour, brickContainerTransform = this.brickContainerTransform, brickPrefab = this.brickPrefab, paddleBehaviour = this.paddleBehaviour
        };
        
        gameManager.Initialize(items);
    }

    public override void OnPreDestroy() {
        uiManagerDefault.Unload("game_screen");
        base.OnPreDestroy();
    }

}
