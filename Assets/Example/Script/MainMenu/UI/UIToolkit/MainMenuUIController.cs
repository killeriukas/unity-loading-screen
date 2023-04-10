using System;
using TMI.Core;
using TMI.SceneManagement;
using TMI.UI;
using UnityEngine;
using UnityEngine.UIElements;
using UIController = TMI.UI.UIToolkit.UIController;
using UIManager = TMI.UI.UIToolkit.UIManager;

namespace Example.UI.UIToolkit {
	
	public class MainMenuUIController : UIController {

		private class OptionScreenController : IDisposable {

			private readonly VisualElement root;
			
			private readonly Button closeButton;
			private readonly Label descriptionText;

			public OptionScreenController(VisualElement rootVisualElement, OptionScreenDataSO optionScreenData) {
				descriptionText = rootVisualElement.Q<Label>(optionScreenData.descriptionTextId);
				descriptionText.text = optionScreenData.descriptionText;
				
				closeButton = rootVisualElement.Q<Button>(optionScreenData.closeButtonId);
				closeButton.clicked += Hide;
				
				root = rootVisualElement;
			}

			public void Show() {
				root.style.display = DisplayStyle.Flex;
			}

			public void Hide() {
				root.style.display = DisplayStyle.None;
			}
			
			public void Dispose() {
				closeButton.clicked -= Hide;
			}

		}

		[SerializeField]
		private MainMenuScreenDataSO mainMenuScreenData;

		[SerializeField]
		private OptionScreenDataSO optionScreenData;

		private OptionScreenController optionScreenController;
		
		private Button startButton;
		private Button optionButton;
		private Button quitButton;

		private IUIManager uiManager;
		private ISceneManager sceneManager;
		
		public override void Setup(IInitializer initializer) {
			base.Setup(initializer);
			uiManager = initializer.GetManager<UIManager, IUIManager>();
			sceneManager = initializer.GetManager<SceneManager, ISceneManager>();
			
			startButton = rootVisualElement.Q<Button>(mainMenuScreenData.startButtonId);
			optionButton = rootVisualElement.Q<Button>(mainMenuScreenData.optionButtonId);
			quitButton = rootVisualElement.Q<Button>(mainMenuScreenData.quitButtonId);

			VisualElement optionScreenVisualElement = rootVisualElement.Q<VisualElement>(mainMenuScreenData.optionScreenId);
			optionScreenController = new OptionScreenController(optionScreenVisualElement, optionScreenData);

			startButton.clicked += OnStartButtonClicked;
			optionButton.clicked += OnOptionButtonClicked;
			quitButton.clicked += OnQuitButtonClicked;
		}

		private void OnStartButtonClicked() {
			LoadingScreenUIController loadingScreenUIController = uiManager.Load<LoadingScreenUIController>();
			loadingScreenUIController.Show();

			sceneManager.LoadAsync(SceneConstant.game);
		}

		private void OnOptionButtonClicked() {
			optionScreenController.Show();
		}

		private void OnQuitButtonClicked() {
			Application.Quit();
		}

		protected override void OnDestroy() {
			optionScreenController.Dispose();
			
			startButton.clicked -= OnStartButtonClicked;
			quitButton.clicked -= OnQuitButtonClicked;
			optionButton.clicked -= OnOptionButtonClicked;
			base.OnDestroy();
		}
		
	}
	
}


