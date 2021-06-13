using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpEffectMonitor : MonoBehaviour
{
    Timer speedUpTimer;
    private float _speedUpFactor;

    public bool SpeedUpActive
    {
        get
        {
            return speedUpTimer.Running;
        }
    }

    public float TimeLeft
    {
        get
        {
            return speedUpTimer.TimeLeft;
        }
    }

    public float SpeedUpFactor
    {
        get { return _speedUpFactor; }
    }


    void Start()
    {
        speedUpTimer = gameObject.AddComponent<Timer>();
        speedUpTimer.AddTimerFinishedListener(HandleSpeedUpTimerFinished);

        EventManager.AddSpeedUpEffectListener(SpeedUpActivated);
    }

    private void SpeedUpActivated(float factor, float duration)
    {
        if(!speedUpTimer.Running)
        {
            _speedUpFactor = factor;
            speedUpTimer.Duration = duration;
            speedUpTimer.Run();
        } else
        {
            speedUpTimer.AddTime(duration);
        }
    }
 
    private void HandleSpeedUpTimerFinished()
    {
        speedUpTimer.Stop();
        _speedUpFactor = 1f;
    }
}
