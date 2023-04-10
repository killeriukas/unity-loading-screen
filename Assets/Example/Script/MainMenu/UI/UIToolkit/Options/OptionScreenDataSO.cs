using UnityEngine;

[CreateAssetMenu(fileName = "OptionScreenData", menuName = "Example/UI/MainMenu/OptionScreenData", order = 0)]
public class OptionScreenDataSO : ScriptableObject {
    public string descriptionTextId;
    public string closeButtonId;

    public string descriptionText;
}