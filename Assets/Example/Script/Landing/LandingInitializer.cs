using System;
using System.Collections;
using Example.UI.UIToolkit;
using TMI.AssetManagement;
using TMI.ConfigManagement.Unity.UI;
using TMI.Core;
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
        IAssetManager assetManager = GetManager<AssetManager, IAssetManager>();

        LoadingScreenUIController loadingScreen = uiManagerDefault.Load<LoadingScreenUIController>();
        loadingScreen.version = "Version: " + Application.version;

        Action<ILoaderComplete> onComplete = (data) => { sceneManager.LoadAsync(SceneConstant.main_menu); };

        //start - create fake loader
        IRequestHandler requestHandler = RequestHandler.Create(onComplete);
        FakeGroup fakeGroup = new FakeGroup(TimeSpan.FromSeconds(10f));
        IHandle handle = assetManager.LoadAsync(fakeGroup, requestHandler);
        loadingScreen.Setup(handle);
        //end - create fake loader
        
        loadingScreen.Show();
    }

}