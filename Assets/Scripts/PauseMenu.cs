using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PauseMenus
{
    public string id;
    public GameObject menu;
}

public class PauseMenu : MonoBehaviour
{
    public GameObject currentMenu;
    public List<PauseMenus> pauseMenus = new();

    public void SwitchMenu(string id)
    {
        currentMenu.SetActive(false);
        currentMenu = pauseMenus.Find(menu => menu.id == id).menu;
        currentMenu.SetActive(true);
    }
}
