using TMPro;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    public TextMeshProUGUI commandText;

    public void ClearText()
    {
        commandText.text = "";
    }

    public void AddText(string additionalText)
    {
        commandText.text += additionalText;
    }
}
