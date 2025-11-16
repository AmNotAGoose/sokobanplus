using UnityEngine;

public class Menu : MonoBehaviour
{
    public AudioManager audioManager;

    private void Start()
    {
        audioManager.PlaySound("music:menu", true);
    }
}
