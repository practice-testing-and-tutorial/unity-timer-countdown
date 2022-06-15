using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Timer _timerPrefab;

    private Timer _timer;

    private void Awake() 
    {
        _timer = Instantiate(_timerPrefab, Vector3.zero, Quaternion.identity, _canvas.transform);

        _timer.Initialize(10f);

        _timer.TimerEnded += OnTimerEnded;
    }

    private void OnDestroy() {
        _timer.TimerEnded -= OnTimerEnded;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _timer.Begin();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (_timer.IsPaused)
            {
                _timer.Continue();
            }
            else
            {
                _timer.Pause();
            }
        }
    }

    private void OnTimerEnded(object sender, EventArgs e)
    {
        #if UNITY_EDITOR
        Debug.Log("Timer Ended");
        #endif
    }
}
