using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A timer
/// </summary>
public class Timer : MonoBehaviour
{
	#region Fields

	// timer duration
	float totalSeconds = 0;

	// timer execution
	float elapsedSeconds = 0;

	float timeLeft;
	bool running = false;

	// support for Finished property
	bool started = false;

	TimerFinishedEvent timerFinishedEvent = new TimerFinishedEvent();

	#endregion

	#region Properties

	/// <summary>
	/// Sets the duration of the timer
	/// The duration can only be set if the timer isn't currently running
	/// </summary>
	/// <value>duration</value>
	public float Duration
	{
		set
		{
			if (!running)
			{
				totalSeconds = value;
			}
		}
	}

	public float TimeLeft
    {
		get
        {
			if(Finished)
            {
				return totalSeconds;
            } else
            {
				return timeLeft;
            }
        }
    }

	/// <summary>
	/// Gets whether or not the timer has finished running
	/// This property returns false if the timer has never been started
	/// </summary>
	/// <value>true if finished; otherwise, false.</value>
	public bool Finished
	{
		get { return started && !running; }
	}

	/// <summary>
	/// Gets whether or not the timer is currently running
	/// </summary>
	/// <value>true if running; otherwise, false.</value>
	public bool Running
	{
		get { return running; }
	}

	#endregion

	#region Methods

	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		// update timer and check for finished
		if (running)
		{
			elapsedSeconds += Time.deltaTime;
			timeLeft = totalSeconds - elapsedSeconds;
			if (elapsedSeconds >= totalSeconds)
			{
				running = false;
				timerFinishedEvent.Invoke();
			}
		}
	}

	/// <summary>
	/// Runs the timer
	/// Because a timer of 0 duration doesn't really make sense,
	/// the timer only runs if the total seconds is larger than 0
	/// This also makes sure the consumer of the class has actually 
	/// set the duration to something higher than 0
	/// </summary>
	public void Run()
	{
		// only run with valid duration
		if (totalSeconds > 0)
		{
			started = true;
			running = true;
			elapsedSeconds = 0;
		}
	}

	public void Stop()
	{
		totalSeconds = 0;
		elapsedSeconds = 0;
		running = false;
		started = false;
	}

	public void AddTime(float time)
    {
		totalSeconds += time;
    }

	public void AddTimerFinishedListener(UnityAction listener)
    {
		timerFinishedEvent.AddListener(listener);
    }


    #endregion
}
