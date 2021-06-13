using System.Collections;
using UnityEngine;


public class Paddle : MonoBehaviour
{

    #region Fields

    private float halfColliderWidth;
    private float halfColliderHeight;
    private const float bounceAngleHalfRange = 60 * Mathf.Deg2Rad;
    private Rigidbody2D rb;
    private BoxCollider2D bc2d;

    private bool _isFrozen = false;

    Timer freezeTimer;

    #endregion

    #region Methods

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        CalculateHalfColliderSize();

        freezeTimer = gameObject.AddComponent<Timer>();
        freezeTimer.AddTimerFinishedListener(HandleFreezeTimerFinished);

        EventManager.AddFreezerEffectListener(FreezeThePaddle);
    }

    private void FixedUpdate()
    {
        if(!_isFrozen)
        {
            MovePaddle();
        }
    }

    private void MovePaddle()
    {
        float inputX = Input.GetAxis("Horizontal");
        if(inputX != 0)
        {
            Vector2 position = rb.position;
            position.x += inputX * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.deltaTime;
            position.x = CalculateClampedX(position.x);
            rb.MovePosition(position);
        }
    }

    private void CalculateHalfColliderSize()
    {
        halfColliderWidth = bc2d.size.x * 0.5f;
        halfColliderHeight = bc2d.size.y * 0.5f;
    }

    private float CalculateClampedX(float x)
    {
        float colliderLeftEdge = x - halfColliderWidth;
        float colliderRightEdge = x + halfColliderWidth;

        if (colliderLeftEdge < ScreenUtils.ScreenLeft)
        {
            x = ScreenUtils.ScreenLeft + halfColliderWidth;
        }
        else if(colliderRightEdge > ScreenUtils.ScreenRight)
        {
            x = ScreenUtils.ScreenRight - halfColliderWidth;
        }
        return x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball") && IsPaddleTop(collision))
        {
            AudioManager.Play(AudioClipName.PaddleHit);
            float ballOffsetFromPaddleCenter = transform.position.x - collision.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter / halfColliderWidth;
            float angleOffset = normalizedBallOffset * bounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            Ball ballScript = collision.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }


    private bool IsPaddleTop(Collision2D ball)
    {
        ContactPoint2D contactPoint = ball.GetContact(0);

        float tolerance = 0.05f;
        float topPaddlePositionY = transform.position.y + halfColliderHeight - tolerance;

        if (contactPoint.point.y >= topPaddlePositionY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FreezeThePaddle(float freezeDuration)
    {
        _isFrozen = true;
        if(!freezeTimer.Running)
        {
            freezeTimer.Duration = freezeDuration;
            freezeTimer.Run();
        } else
        {
            freezeTimer.AddTime(freezeDuration);
        }
    }

    private void HandleFreezeTimerFinished()
    {
        _isFrozen = false;
        freezeTimer.Stop();
    }

    #endregion
}