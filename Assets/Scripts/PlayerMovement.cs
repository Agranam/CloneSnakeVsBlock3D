using System.Collections;
using UnityEngine;

public enum PlayerState
{
    MovingInBackground,
    MovingInGame,
    Stay,
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerState CurrentPlayerState;
    [SerializeField] private float _sensity;
    [SerializeField] private float _gameSpeedMoving = 20;
    [SerializeField] private float _menuSpeedMoving = 5;
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody _rigidbody;
    
    private float _currentSpeedMoving;
    private float _sidewaysSpeed1;
    private Vector3 _touchLastPos;
    private float _sidewaysSpeed;
    private bool _isSlowMoving = true;
    private Coroutine _runningCoroutine;
    
    private void Start()
    {
        _currentSpeedMoving = _menuSpeedMoving;
    }

    private void Update()
    {
        switch (CurrentPlayerState)
        {
            case PlayerState.Stay:
                _currentSpeedMoving = 0;
                _rigidbody.isKinematic = true;
                break;
            case PlayerState.MovingInBackground:
                _rigidbody.isKinematic = false;

                if (!_isSlowMoving)
                {
                    if (_runningCoroutine != null)
                        StopCoroutine(_runningCoroutine);
                    _runningCoroutine = StartCoroutine(SmoothChange(_currentSpeedMoving, _menuSpeedMoving, 5));
                    _isSlowMoving = true;
                }
                break;
            case PlayerState.MovingInGame:
                PcControl();
                _rigidbody.isKinematic = false;

                if (_isSlowMoving)
                {
                    if (_runningCoroutine != null)
                        StopCoroutine(_runningCoroutine);
                    _runningCoroutine = StartCoroutine(SmoothChange(_currentSpeedMoving, _gameSpeedMoving, 5));
                    _isSlowMoving = false;
                }
                else
                {
                    _currentSpeedMoving = _gameSpeedMoving;
                }
                break;
        }
    }

    private IEnumerator SmoothChange(float startValue, float endValue, float duration)
    {
        float time = 0;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            time += Time.deltaTime * (1f / duration);
            _currentSpeedMoving = Mathf.Lerp(startValue, endValue, time);
            yield return null;
        }
    }
    
    public void SetState(PlayerState currentState)
    {
        CurrentPlayerState = currentState;
    }

    private void PcControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _touchLastPos = _camera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = _camera.ScreenToViewportPoint(Input.mousePosition) - _touchLastPos;
            _sidewaysSpeed += delta.x * _sensity;
            _touchLastPos = _camera.ScreenToViewportPoint(Input.mousePosition);
        }
    }
    
    private void FixedUpdate()
    {
        if (CurrentPlayerState == PlayerState.Stay) return;
        
        if (Mathf.Abs(_sidewaysSpeed) > 4) _sidewaysSpeed = 4 * Mathf.Sign(_sidewaysSpeed);
        _rigidbody.velocity = new Vector3(_sidewaysSpeed * 5, 0f, _currentSpeedMoving);
        _sidewaysSpeed = 0;
    }
}
