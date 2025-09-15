using UnityEngine;

public class Timer : MonoBehaviour
{
    public int TimerBaseTime = 0;
    public float TimerCurrentTime;
    public bool StartTimer = false;

    public bool ReachingTimerMaxValue = false;
    public float MaxTimerValue;

    public void StartCountdown()
    {
        StartTimer = true;
    }
    public void PauseCountdown()
    {
        StartTimer = false;
    }
    public void StopCountdown()
    {
        StartTimer = false;
        TimerCurrentTime = TimerBaseTime;
        ReachingTimerMaxValue = false;
    }

    void Update()
    {
        if (StartTimer == true)
        {
            TimerCurrentTime += Time.deltaTime;

            if (TimerCurrentTime > MaxTimerValue)
            {
                ReachingTimerMaxValue = true;
            }
        }
    }
}
