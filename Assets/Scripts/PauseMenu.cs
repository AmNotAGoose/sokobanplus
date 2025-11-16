using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public GameObject menuObject;


    public void TogglePauseGame()
    {
        menuObject.SetActive(!menuObject.activeSelf);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }

    public void SwitchMenu(string id)
    {
        currentMenu.SetActive(false);
        currentMenu = pauseMenus.Find(menu => menu.id == id).menu;
        currentMenu.SetActive(true);
    }
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

