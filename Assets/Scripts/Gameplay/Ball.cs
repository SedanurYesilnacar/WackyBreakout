using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D cc2d;

    // The ball should wait before moving when it spawned
    private float _ballWaitingTime = 1f;


    BallSpawner ballSpawner;

    Timer ballSpawnTimer;
    Timer ballDeathTimer;

    Timer speedUpTimer;
    private float _ballSpeedUpFactor;

    BallLeftEvent ballLeftEvent;

    BallDiedEvent ballDiedEvent;


    private void Start()
    {
        SpawnTimeProcessing();
        DeathTimeProcessing();

        ballDeathTimer.AddTimerFinishedListener(HandleDeathTimerFinished);
        ballSpawnTimer.AddTimerFinishedListener(HandleSpawnTimerFinished);

        rb = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        ballSpawner = Camera.main.GetComponent<BallSpawner>();


        speedUpTimer = gameObject.AddComponent<Timer>();
        speedUpTimer.AddTimerFinishedListener(HandleSpeedUpTimerFinished);
        EventManager.AddSpeedUpEffectListener(SpeedUp);

        ballLeftEvent = new BallLeftEvent();
        EventManager.AddBallLeftInvoker(this);

        ballDiedEvent = new BallDiedEvent();
        EventManager.AddBallDiedInvoker(this);

    }

    private void DeathTimeProcessing()
    {
        ballDeathTimer = gameObject.AddComponent<Timer>();
        ballDeathTimer.Duration = ConfigurationUtils.BallLifetime;
        ballDeathTimer.Run();
    }

    private void SpawnTimeProcessing()
    {
        ballSpawnTimer = gameObject.AddComponent<Timer>();
        ballSpawnTimer.Duration = _ballWaitingTime;
        ballSpawnTimer.Run();
    }

    private void Move()
    {
        float angle = -90 * Mathf.Deg2Rad;
        float forceX = Mathf.Cos(angle) * ConfigurationUtils.BallImpulseForce;
        float forceY = Mathf.Sin(angle) * ConfigurationUtils.BallImpulseForce;
        Vector2 force = new Vector2(forceX,forceY);
        if(EffectUtils.SpeedUpActive)
        {
            SpeedUp(EffectUtils.SpeedUpFactor, EffectUtils.TimeLeft);
            force *= _ballSpeedUpFactor;
        }
        rb.AddForce(force);
    }

    public void SetDirection(Vector2 direction)
    {
        rb.velocity = direction * rb.velocity.magnitude;
    }

    private void OnBecameInvisible()
    {
        if(!ballDeathTimer.Finished)
        {
            float colliderRadius = cc2d.radius;
            if(transform.position.y - colliderRadius < ScreenUtils.ScreenBottom)
            {
                ballLeftEvent.Invoke();
            }
            Destroy(gameObject);
        }
    }

    private void SpeedUp(float speedUpFactor, float speedUpDuration)
    {
        if(!speedUpTimer.Running)
        {
            _ballSpeedUpFactor = speedUpFactor;
            speedUpTimer.Duration = speedUpDuration;
            speedUpTimer.Run();
            rb.velocity *= _ballSpeedUpFactor;
        } else
        {
            speedUpTimer.AddTime(speedUpDuration);
        }
    }

    public void AddBallLeftListener(UnityAction listener)
    {
        ballLeftEvent.AddListener(listener);
    }

    public void AddBallDiedListener(UnityAction listener)
    {
        ballDiedEvent.AddListener(listener);
    }

    private void HandleSpeedUpTimerFinished()
    {
        speedUpTimer.Stop();
        rb.velocity /= _ballSpeedUpFactor;
    }

    private void HandleDeathTimerFinished()
    {
        ballDiedEvent.Invoke();
        Destroy(gameObject);
    }

    private void HandleSpawnTimerFinished()
    {
        ballSpawnTimer.Stop();
        Move();
    }
}
