using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool canPause = false;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        canPause = !(scene.name == "Menu");

        if (!canPause) pauseMenu.SetActive(false);
    }
    public void TogglePauseGame()
    {
        if (!canPause) return;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
