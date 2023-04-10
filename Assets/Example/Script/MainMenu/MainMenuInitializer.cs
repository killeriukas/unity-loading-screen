using Example.UI.UIToolkit;
using TMI.AssetManagement;
using TMI.ConfigManagement.Unity.UI;
using TMI.Core.Unity;

public class MainMenuInitializer : Initializer {

    protected override void OnUIAssetsCached() {
        LoadingScreenUIController loadingScreen = uiManagerDefault.Load<LoadingScreenUIController>(false);
        loadingScreen.Hide();

        MainMenuUIController mainMenuUIController = uiManagerDefault.Load<MainMenuUIController>();
        mainMenuUIController.Show();
    }

    public override void OnPreDestroy() {
        uiManagerDefault.Unload("main_menu_screen");
        base.OnPreDestroy();
    }

    protected override IGroup CreateUIAssetCacheGroup() {
        IUIConfig uiConfig = uiManagerDefault.GetConfig();
        
        UIConfigGroup assetGroup = new UIConfigGroup(uiConfig);
        assetGroup.Add("main_menu_screen");
        return assetGroup;
	}

}
