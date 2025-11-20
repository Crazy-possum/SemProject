using UnityEngine;

public class Timer
{
    private int _timerBaseTime = 0;
    private float _timerCurrentTime;
    private bool _isStartTimer = false;

    private bool _isReachingTimerMaxValue = false;
    private float _maxTimerValue;

    public bool ReachingTimerMaxValue { get => _isReachingTimerMaxValue; set => _isReachingTimerMaxValue = value; }
    public bool StartTimer { get => _isStartTimer; set => _isStartTimer = value; }
    public float TimerCurrentTime { get => _timerCurrentTime; set => _timerCurrentTime = value; }

    public Timer(float maxTimerValue) 
    {
        _maxTimerValue = maxTimerValue;
    }

    public void ResetTimerMaxTime(float maxTimerValue)
    {
        _maxTimerValue = maxTimerValue;
    }

    public void StartCountdown()
    {
        _isStartTimer = true;
    }

    public void PauseCountdown()
    {
        _isStartTimer = false;
    }

    public void StopCountdown()
    {
        _isStartTimer = false;
        _timerCurrentTime = _timerBaseTime;
        _isReachingTimerMaxValue = false;
    }

    public void Wait()
    {
        if (_isStartTimer == true)
        {
            _timerCurrentTime += Time.deltaTime;

            if (_timerCurrentTime > _maxTimerValue)
            {
                _isReachingTimerMaxValue = true;
            }
        }
    }
}
