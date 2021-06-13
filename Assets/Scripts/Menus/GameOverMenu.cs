using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Text _scoreTxt;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void SetScore(int score)
    {
        _scoreTxt.text = "SCORE: " + score.ToString();
    }

    public void MenuBtn()
    {
        AudioManager.Play(AudioClipName.ButtonClick);
        Time.timeScale = 1;
        MenuManager.GoToMenu(MenuName.Main);
        Destroy(gameObject);
    }
}
