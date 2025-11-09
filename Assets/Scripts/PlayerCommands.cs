using TMPro;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    public TextMeshProUGUI commandText;

    public string GetAndClearText()
    {
        string curText = commandText.text;
        commandText.SetText("");
        return curText;
    }

    public void AddText(string additionalText)
    {
        commandText.SetText(commandText.text + additionalText);
    }
}
