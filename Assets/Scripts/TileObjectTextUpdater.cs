using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class TileObjectTextUpdater : MonoBehaviour
{
    public List<TMP_Text> texts;
    
    public void UpdateText(int index, string text)
    {
        texts[index].SetText(text);
    }
}
