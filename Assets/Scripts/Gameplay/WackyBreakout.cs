using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackyBreakout : MonoBehaviour
{
    private void Start()
    {
        EventManager.AddLastBallLeftListener(HandleLastBallLeftEvent);
        EventManager.AddBlockDestroyedListener(HandleLastBallLeftEvent);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.GoToMenu(MenuName.Pause);
        }
    }

    private void HandleLastBallLeftEvent()
    {
        EndGame();
    }

    private void EndGame()
    {
        AudioManager.Play(AudioClipName.GameOver);
        GameObject gameOverScreen = Object.Instantiate(Resources.Load("GameOverScreen")) as GameObject;
        GameOverMenu gameOverScript = gameOverScreen.GetComponent<GameOverMenu>();
        HUD hudScript = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
        gameOverScript.SetScore(hudScript.Score);
    }
}
