using UnityEngine;

public enum PlayerState
{
    Moving,
    Stay,
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerState CurrentPlayerState { get; private set; }
    [SerializeField] private float _sensity;
    [Range(0f, 10f)] [SerializeField] private float _speedMoving;
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody _rigidbody;
    
    private float _sidewaysSpeed1;
    private Vector3 _touchLastPos;
    private float _sidewaysSpeed;

    private void Update()
    {
        PcControl();
        if (CurrentPlayerState == PlayerState.Stay)
        {
            _speedMoving = 0;
            _rigidbody.isKinematic = true;
            CurrentPlayerState = PlayerState.Stay;
        }
        if (CurrentPlayerState == PlayerState.Moving)
        {
            _rigidbody.isKinematic = false;
            _speedMoving = 5;
            CurrentPlayerState = PlayerState.Moving;
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
        _rigidbody.velocity = new Vector3(_sidewaysSpeed * 5, 0f, _speedMoving);
        _sidewaysSpeed = 0;
    }
}
