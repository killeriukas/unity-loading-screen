using TMI.AssetManagement;
using TMI.Core;
using TMI.UI.UIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

namespace Example.UI.UIToolkit {

    public class LoadingScreenUIController : UIController, IUpdatable {

        private Label versionText;
        
        
        
        // [SerializeField]
        // private UITextPro loadingText;
        //
        // [SerializeField]
        // private UISlider loadingBar;

        // [SerializeField]
        // private UITextPro versionText;

        private const string defaultLoadingText = "Loading...";

        // public string text {
        //     set {
        //         loadingText.text = value;
        //     }
        // }
        //
        // public float loadingBarValue {
        //     set {
        //         loadingBar.value = value;
        //     }
        // }

        public string version {
            set {
                versionText.text = value;
            }
        }

        // public override void Refresh() {
        //     base.Refresh();
        //     loadingText.text = defaultLoadingText;
        //     loadingBarValue = 0f;
        // }

        private IExecutionManager executionManager;
        private IHandle handle;

        public override void Setup(IInitializer initializer) {
            base.Setup(initializer);
            this.executionManager = initializer.GetManager<ExecutionManager, IExecutionManager>();
            versionText = rootVisualElement.Q<Label>("version");
        }

        public void Setup(IHandle handle) {
            this.handle = handle;
            executionManager.Register(this, OnUpdate);
        }

        private ExecutionManager.Result OnUpdate() {
            float progress = handle.progress;
           // loadingBarValue = progress;
            if(progress < 1f) {
                return ExecutionManager.Result.Continue;
            } else {
                handle = null;
                return ExecutionManager.Result.Finish;
            }
            
        }

	}

}