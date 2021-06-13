using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _ballPrefab;

    // To instantiate ball every 5-10 secs
    private float _randomSpawnTime;

    private float _minBallSpawnTime;
    private float _maxBallSpawnTime;

    // To check ball collision
    private float _ballRadius;

    private Vector2 _ballBottomLeftCorner;
    private Vector2 _ballTopRightCorner;



    private bool retrySpawn = false;

    Timer randomBallSpawnTimer;



    private void Start()
    {
        GameObject tempBall = Instantiate<GameObject>(_ballPrefab);
        _ballRadius = tempBall.GetComponent<CircleCollider2D>().radius;
        _ballBottomLeftCorner = new Vector2(tempBall.transform.position.x - _ballRadius, tempBall.transform.position.y - _ballRadius);
        _ballTopRightCorner = new Vector2(tempBall.transform.position.x + _ballRadius, tempBall.transform.position.y + _ballRadius);
        Destroy(tempBall);
        _minBallSpawnTime = ConfigurationUtils.MinBallSpawnTime;
        _maxBallSpawnTime = ConfigurationUtils.MaxBallSpawnTime;
        
        randomBallSpawnTimer = gameObject.AddComponent<Timer>();
        randomBallSpawnTimer.AddTimerFinishedListener(HandleRandomBallSpawnTimer);

        EventManager.AddBallLeftListener(SpawnBall);
        EventManager.AddBallDiedListener(SpawnBall);

        SpawnBallOnTime();
        SpawnBall();
    }

    private void Update()
    {
        if(retrySpawn)
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        retrySpawn = false;

        if(Physics2D.OverlapArea(_ballBottomLeftCorner,_ballTopRightCorner) == null)
        {
            retrySpawn = false;
            Instantiate(_ballPrefab);
        } else
        {
            retrySpawn = true;
        }
    }

    private void SpawnBallOnTime()
    {
        _randomSpawnTime = Random.Range(_minBallSpawnTime, _maxBallSpawnTime);
        randomBallSpawnTimer.Duration = _randomSpawnTime;
        randomBallSpawnTimer.Run();
    }

    private void HandleRandomBallSpawnTimer()
    {
        retrySpawn = false;
        SpawnBall();
        SpawnBallOnTime();
    }
}
