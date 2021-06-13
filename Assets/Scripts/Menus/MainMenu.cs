using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitButton()
    {
        AudioManager.Play(AudioClipName.ButtonClick);
        Application.Quit();
    }

    public void HelpButton()
    {
        AudioManager.Play(AudioClipName.ButtonClick);
        MenuManager.GoToMenu(MenuName.Help);
    }

    public void PlayButton()
    {
        AudioManager.Play(AudioClipName.ButtonClick);
        SceneManager.LoadScene("Gameplay");
    }
}