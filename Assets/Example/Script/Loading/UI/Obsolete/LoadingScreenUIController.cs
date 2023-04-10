using TMI.AssetManagement;
using TMI.Core;
using TMI.UI;
using UnityEngine;

namespace Example.UI {

    public class LoadingScreenUIController : UIController, IUpdatable {

        [SerializeField]
        private UITextPro loadingText;

        [SerializeField]
        private UISlider loadingBar;

        [SerializeField]
        private UITextPro versionText;

        private const string defaultLoadingText = "Loading...";

        public string text {
            set {
                loadingText.text = value;
            }
        }

        public float loadingBarValue {
            set {
                loadingBar.value = value;
            }
        }

        public string version {
            set {
                versionText.text = value;
            }
        }

        public override void Refresh() {
            base.Refresh();
            loadingText.text = defaultLoadingText;
            loadingBarValue = 0f;
        }

        private IExecutionManager executionManager;
        private IHandle handle;

		public override void Setup(IInitializer initializer) {
			base.Setup(initializer);
            this.executionManager = initializer.GetManager<ExecutionManager, IExecutionManager>();
		}

		public void Setup(IHandle handle) {
            this.handle = handle;
            executionManager.Register(this, OnUpdate);
        }

        private ExecutionManager.Result OnUpdate() {
            float progress = handle.progress;
            loadingBarValue = progress;
            if(progress < 1f) {
                return ExecutionManager.Result.Continue;
            } else {
                handle = null;
                return ExecutionManager.Result.Finish;
            }
            
        }

	}

}