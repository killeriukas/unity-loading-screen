using TMI.AssetManagement;
using TMI.Core;
using TMI.UI.UIToolkit;
using UnityEngine.UIElements;

namespace Example.UI.UIToolkit {

    public class LoadingScreenUIController : UIController, IUpdatable {

        private Label versionText;
        private ProgressBar loadingBar;

        public string version {
            set {
                versionText.text = value;
            }
        }

        public override void Refresh() {
            base.Refresh();
            loadingBar.value = 0f;
            loadingBar.title = "0%";
        }

        private IExecutionManager executionManager;
        private IHandle handle;

        public override void Setup(IInitializer initializer) {
            base.Setup(initializer);
            this.executionManager = initializer.GetManager<ExecutionManager, IExecutionManager>();
            versionText = rootVisualElement.Q<Label>("version");
            loadingBar = rootVisualElement.Q<ProgressBar>("loading");
        }

        public void Setup(IHandle handle) {
            this.handle = handle;
            executionManager.Register(this, OnUpdate);
        }

        private ExecutionManager.Result OnUpdate() {
            float progress = handle.progress;
            loadingBar.value = progress * 100f;
            loadingBar.title = (int)loadingBar.value + "%";
            if(progress < 1f) {
                return ExecutionManager.Result.Continue;
            } else {
                handle = null;
                return ExecutionManager.Result.Finish;
            }
            
        }

	}

}