using System;
using TMI.Core;
using TMI.UI.UIToolkit;
using UnityEngine;
using UnityEngine.UIElements;

namespace Example.UI.UIToolkit {
 
    public class GameUIController : UIController {
        
        [SerializeField]
        private VisualTreeAsset lifeItemVTA;

        [SerializeField]
        private VisualTreeAsset gameOverScreenVTA;
        
        [SerializeField]
        private VisualTreeAsset gameWonScreenVTA;
        
        //game screen - start
        private VisualElement livesContainer;
        private VisualElement overlayContainer;
        
        private Label scoreLabel;
        //game screen - end

        //game over screen - start
        private Label totalScoreLabel;
        private Button restartButton;
        private Button quitButton;
        
        public event Action onRestartClicked {
            add {
                restartButton.clicked += value;
            }
            remove {
                restartButton.clicked -= value;
            }
        }
        
        public event Action onQuitClicked {
            add {
                quitButton.clicked += value;
            }
            remove {
                quitButton.clicked -= value;
            }
        }
        //game over screen - end
        
        //game won screen - start
        private Button continueButton;
        
        public event Action onContinueClicked {
            add {
                continueButton.clicked += value;
            }
            remove {
                continueButton.clicked -= value;
            }
        }
        //game won screen - end
        
        public override void Setup(IInitializer initializer) {
            base.Setup(initializer);
            overlayContainer = rootVisualElement.Q<VisualElement>("overlay");
            livesContainer = rootVisualElement.Q<VisualElement>("lives-container");

            scoreLabel = rootVisualElement.Q<Label>("score-label");

            overlayContainer.style.display = DisplayStyle.None;
            
            Listen<LifeLostNotification>(this, OnLifeLost);
            Listen<ScoreIncreasedNotification>(this, OnScoreIncreased);
        }
        
        private void OnScoreIncreased(ScoreIncreasedNotification notification) {
            scoreLabel.text = "Score: " + notification.newValue;
        }

        public void Initialize(GameController gameController) {

            for(int i = 0; i < GameController.STARTING_LIVES; ++i) {
                lifeItemVTA.CloneTree(livesContainer);
            }

            scoreLabel.text = "Score: " + GameController.STARTING_SCORE;
        }

        public void ShowGameWonScreen(int totalScore) {
            gameWonScreenVTA.CloneTree(overlayContainer);
            continueButton = overlayContainer.Q<Button>("continue-button");
            totalScoreLabel = overlayContainer.Q<Label>("score-label");
            
            totalScoreLabel.text = "Current Score: " + totalScore;
            
            overlayContainer.style.display = DisplayStyle.Flex;
        }

        public void CloseGameWonScreen() {
            overlayContainer.RemoveAt(0);
            overlayContainer.style.display = DisplayStyle.None;
        }

        public void ShowGameOverScreen(int totalScore) {
            gameOverScreenVTA.CloneTree(overlayContainer);
            
            restartButton = overlayContainer.Q<Button>("restart-button");
            quitButton = overlayContainer.Q<Button>("quit-button");
            totalScoreLabel = overlayContainer.Q<Label>("score-label");

            totalScoreLabel.text = "Total Score: " + totalScore;

            overlayContainer.style.display = DisplayStyle.Flex;
        }

        public void CloseGameOverScreen() {
            overlayContainer.RemoveAt(0);
            overlayContainer.style.display = DisplayStyle.None;
        }
        
        private void OnLifeLost(LifeLostNotification notifification) {
            livesContainer.RemoveAt(livesContainer.childCount - 1);
        }

        protected override void OnDestroy() {
            StopListen<LifeLostNotification>(this);
            StopListen<ScoreIncreasedNotification>(this);
            base.OnDestroy();
        }

    }
    
}


