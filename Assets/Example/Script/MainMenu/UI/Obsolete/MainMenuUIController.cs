using TMI.Core;
using TMI.SceneManagement;
using TMI.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Example.UI {
	public class MainMenuUIController : UIController {

		[SerializeField]
		private UIButton quitButton;

		[SerializeField]
		private UIButton startButton;

		private ISceneManager sceneManager;

		public override void Setup(IInitializer initializer) {
			base.Setup(initializer);
			sceneManager = initializer.GetManager<SceneManager, ISceneManager>();

			quitButton.onButtonClick += OnQuitClicked;
			startButton.onButtonClick += OnStartClicked;
		}

		private void OnStartClicked(PointerEventData data) {
			LoadingScreenUIController loadingScreenUIController = uiManager.Load<LoadingScreenUIController>();
			loadingScreenUIController.Show();

			sceneManager.LoadAsync(SceneConstant.game);
		}

		private void OnQuitClicked(PointerEventData data) {
			Application.Quit();
		}

		protected override void OnDestroy() {
			quitButton.onButtonClick -= OnQuitClicked;
			base.OnDestroy();
		}
	}
}


