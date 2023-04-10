using UnityEngine;

[CreateAssetMenu(fileName = "MainMenuScreenData", menuName = "Example/UI/MainMenu/MainMenuScreenData", order = 0)]
public class MainMenuScreenDataSO : ScriptableObject {
    public string optionScreenId;
		
    public string optionButtonId;
    public string startButtonId;
    public string quitButtonId;
}