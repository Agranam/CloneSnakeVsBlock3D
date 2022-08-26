using UnityEngine;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _speedMoving = 2f;
    [SerializeField] private PlayerMovement _playerMovement;

    private Vector3 _currentRotation;
    private Transform _playerTransform;
    
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
    }

    private void FixedUpdate()
    {
        if (_playerTransform.position.z < 0) return;

        switch (_playerMovement.CurrentPlayerState)
        {
            case PlayerState.MovingInBackground:
                transform.position = CameraMovement(transform.position.z, 
                    _playerTransform.position.z, 28);
                break;
            case PlayerState.MovingInGame:
                transform.position = CameraMovement(transform.position.z, 
                    _playerTransform.position.z - 25, 23);
                break;
        }
    }
    private Vector3 CameraMovement (float startPos, float targetPos, int cameraHeight)
    {
        float lerpPosZ = Mathf.Lerp(startPos, targetPos, _speedMoving * Time.fixedDeltaTime);
        return new Vector3(0f, cameraHeight, lerpPosZ);
    }
}
