using Example.UI.UIToolkit;
using TMI.AssetManagement;
using TMI.ConfigManagement.Unity.UI;
using TMI.Core.Unity;
using UnityEngine;

public class LandingInitializer : Initializer {
	
    protected override IGroup CreateUIAssetCacheGroup() {
	    IUIConfig uiConfig = uiManagerDefault.GetConfig();
	    
	    UIConfigGroup assetGroup = new UIConfigGroup(uiConfig);
	    assetGroup.Add("loading_screen");
        return assetGroup;
    }

    protected override void OnUIAssetsCached() {
	    LoadingScreenUIController loadingScreen = uiManagerDefault.Load<LoadingScreenUIController>();
	    loadingScreen.version = "Version: " + Application.version;
	    loadingScreen.Show();
	    
	    sceneManager.LoadAsync(SceneConstant.main_menu);
    }

}
