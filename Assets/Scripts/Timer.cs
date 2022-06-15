using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public event EventHandler TimerEnded;

    [SerializeField] private Image _fill;

    private bool _hasStarted;
    private bool _isTimerOn;
    private float _remainingDuration;

    public float Duration { get; internal set; }

    public bool IsPaused => !_isTimerOn;

    public void Initialize(float duration)
    {
        Duration = duration;
    }

    public void Begin()
    {
        _remainingDuration = Duration;
        _isTimerOn = true;
        _hasStarted = true;

        _fill.fillAmount = 0f;

        StopCoroutine(UpdateTimerCoroutine());

        StartCoroutine(UpdateTimerCoroutine());
    }

    public void Pause()
    {
        if (_hasStarted)
            _isTimerOn = false;
    }

    public void Continue()
    {
        if (_hasStarted)
        {
            _isTimerOn = true;
            StopCoroutine(UpdateTimerCoroutine());
            StartCoroutine(UpdateTimerCoroutine());
        }
    }

    public void End()
    {
        _hasStarted = false;
        _isTimerOn = false;
        TimerEnded?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        while (_isTimerOn && _remainingDuration > 0f)
        {
            _remainingDuration -= Time.unscaledDeltaTime;

            _fill.fillAmount = Mathf.InverseLerp(0f, Duration, _remainingDuration);
            yield return null;
        }

        if (_remainingDuration <= 0)
        {
            End();
        }
    }
}
