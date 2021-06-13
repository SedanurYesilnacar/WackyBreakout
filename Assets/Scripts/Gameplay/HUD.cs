using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoBehaviour
{
    private static Text _scoreTxt;
    private static Text _remainingBallsTxt;

    private static int _ballsRemaining;
    private static int _score = 0;

    LastBallLeftEvent lastBallLeftEvent;

    public int Score
    {
        get { return _score; }
    }

    private void AddScore(int amount)
    {
        _score += amount;
        _scoreTxt.text = "Score: " + _score.ToString();
    }

    private void BallLeftFromTheScreen()
    {
        _ballsRemaining--;
        _remainingBallsTxt.text = "Balls Left: " + _ballsRemaining.ToString();
        if(_ballsRemaining <= 0)
        {
            lastBallLeftEvent.Invoke();
        }
    }

    private void Start()
    {
        lastBallLeftEvent = new LastBallLeftEvent();

        EventManager.AddAddPointsListener(AddScore);
        EventManager.AddBallLeftListener(BallLeftFromTheScreen);
        EventManager.AddLastBallLeftInvoker(this);
        _ballsRemaining = ConfigurationUtils.NumberOfBallsPerGame;
        _remainingBallsTxt = GameObject.FindGameObjectWithTag("BallsText").GetComponent<Text>();
        _remainingBallsTxt.text = "Balls Left: " + _ballsRemaining.ToString();
        _score = 0;
        _scoreTxt = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        _scoreTxt.text = "Score: " + _score.ToString();
    }

    public void AddLastBallLeftListener(UnityAction listener)
    {
        lastBallLeftEvent.AddListener(listener);
    }
}
