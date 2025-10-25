using UnityEngine;

public class Timer
{
    private int _timerBaseTime = 0;
    private float _timerCurrentTime;
    private bool _startTimer = false;

    private bool _reachingTimerMaxValue = false;
    private float _maxTimerValue;

    public bool ReachingTimerMaxValue { get => _reachingTimerMaxValue; set => _reachingTimerMaxValue = value; }
    public bool StartTimer { get => _startTimer; set => _startTimer = value; }

    public Timer(float maxTimerValue) 
    {
        _maxTimerValue = maxTimerValue;
    }

    public void StartCountdown()
    {
        _startTimer = true;
    }
    public void PauseCountdown()
    {
        _startTimer = false;
    }
    public void StopCountdown()
    {
        _startTimer = false;
        _timerCurrentTime = _timerBaseTime;
        _reachingTimerMaxValue = false;
    }

    public void Wait()
    {
        if (_startTimer == true)
        {
            _timerCurrentTime += Time.deltaTime;

            if (_timerCurrentTime > _maxTimerValue)
            {
                _reachingTimerMaxValue = true;
            }
        }
    }
}
