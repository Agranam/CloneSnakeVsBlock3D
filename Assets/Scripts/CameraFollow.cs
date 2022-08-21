using System.Collections;
using DG.Tweening;
using UnityEngine;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _speedMoving = 2f;
    [SerializeField] private Transform _targetInGame;
    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private bool _isTopPosition = true;
    [SerializeField] private bool _isAnimationContinue = false;
    private Vector3 _currentRotation;
    private Transform _playerTransform;
    private float _counter;
    private Coroutine _runningCoroutine;
    
    private void Start()
    {
        Transform cameraTransform;
        (cameraTransform = transform).rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        cameraTransform.position = new Vector3(0, 28, 0);
        _playerTransform = _playerMovement.GetComponent<Transform>();
    }

    private void Update()
    {
        if (_playerTransform.position.z < 0) return;
            
        _currentRotation = new Vector3(0f, 0f, _playerTransform.position.z);
        transform.LookAt(_currentRotation);        
        
        if (_playerMovement.CurrentPlayerState == PlayerState.MovingInBackground)
        {
            if (transform.eulerAngles.x >= 87)
            {
                transform.rotation = Quaternion.Euler(90, 0, 0);
            }

        }
        if (_playerMovement.CurrentPlayerState == PlayerState.MovingInGame)
        {
        }
    }

    private void FixedUpdate()
    {
        if (_playerTransform.position.z < 0) return;
        
        if (_playerMovement.CurrentPlayerState == PlayerState.MovingInBackground)
        {
            // if (!_isTopPosition)
            // {
            //     if (_runningCoroutine != null)
            //         StopCoroutine(_runningCoroutine);
            //     //StartCoroutine(SetPositionCoroutine(true, 5, true));
            //     //_runningCoroutine = StartCoroutine(SetRotationCoroutineToward(transform.rotation, 90, 3, true));
            //     //_runningCoroutine = StartCoroutine(SetRotationCoroutine(transform.eulerAngles.x,90, 3, true));
            //     _isTopPosition = true;
            // }

            FollowInMenu();
        }
        
        else if (_playerMovement.CurrentPlayerState == PlayerState.MovingInGame)
        {
            // if (_isTopPosition)
            // {
            //     if (_runningCoroutine != null)
            //         StopCoroutine(_runningCoroutine);
            //     //StartCoroutine(SetPositionCoroutine(false, 5, false));
            //     //_runningCoroutine = StartCoroutine(SetRotationCoroutineToward(transform.rotation, 44, 3, false));
            //     //_runningCoroutine = StartCoroutine(SetRotationCoroutine(transform.eulerAngles.x,44, 3, false));
            //     _isTopPosition = false;
            // }

            FollowInGame();
        }
    }

    private void FollowInMenu()
    {
        
        Vector3 transformPosition = Vector3.Lerp(transform.position, _playerTransform.position, _speedMoving * Time.deltaTime);
        transform.position = new Vector3(0f, 28, transformPosition.z);
    }

    private void FollowInGame()
    {
        
        Vector3 transformPosition = Vector3.Lerp(transform.position, _targetInGame.position, _speedMoving * Time.deltaTime);
        transform.position = new Vector3(0f, 23, transformPosition.z);
    }

    private void SetRotation(int angle,int duration, bool topPosition)
    {
        Vector3 newRotation = new Vector3(angle, 0, 0);
        transform.DORotate(newRotation, duration);
        _isTopPosition = topPosition;
    }
    
    private void SetRotationLerp(int angle,int duration, bool topPosition)
    {
        Vector3 newRotation = new Vector3(angle, 0, 0);
        Quaternion newQuaternion = Quaternion.Lerp(transform.rotation, 
            Quaternion.Euler(newRotation), duration * Time.deltaTime);
        transform.rotation = newQuaternion;
        _isTopPosition = topPosition;
    }

    private IEnumerator SetRotationCoroutine(float startAngle, int endAngle, int duration, bool topPosition)
    {
        float time = 0;
        float currentAngle;
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            time += Time.deltaTime * (1f / duration);
            currentAngle = Mathf.Lerp(startAngle, endAngle, time);
            //Vector3 currentRotation = new Vector3(currentAngle, 0, 0);
            //Vector3 newRotation = Vector3.Lerp(Vector3.right * startAngle, Vector3.right * endAngle, time);
            transform.rotation = Quaternion.Euler(currentAngle, 0, 0);
            _isTopPosition = topPosition;

            yield return null;
        }
    }
    private IEnumerator SetRotationCoroutineToward(Quaternion currentRotation, float endAngle, int duration, bool topPosition)
    {
        float time = 0;
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            time += Time.deltaTime * (1f / duration);
            Quaternion targetAngle = Quaternion.Euler (endAngle, 0, 0);
            transform.rotation = Quaternion.Lerp
                (currentRotation, targetAngle, Time.deltaTime * time);

            yield return null;
        }
    }

    private IEnumerator SetPositionCoroutine(bool isMenu, int duration, bool topPosition)
    {
        _isAnimationContinue = true;
        float time = 0;
        
        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            time += Time.deltaTime * (1f / duration);
            Vector3 onBackground = new Vector3(0f, 28, _playerTransform.position.z);
            Vector3 inGame = new Vector3(0f, 23, _targetInGame.position.z);
            
            Vector3 currentTarget = isMenu ? onBackground : inGame;

            transform.position = Vector3.Lerp(transform.position, currentTarget, time);
            _isTopPosition = topPosition;
            yield return null;
        }
        yield return _isAnimationContinue = false;
    }
}
