using UnityEngine;

public enum State
{
    Moving,
    Stay,
}
public class PlayerMovement : MonoBehaviour
{
    public State CurrentState;
    [SerializeField] private float sensity;
    [Range(0f, 10f)]
    [SerializeField] private float speedMoving;
    [Range(0f, 1f)]
    [SerializeField] private float timeScale;
    [SerializeField] private new Camera camera;
    private float _sidewaysSpeed;
    private Vector3 _touchLastPos;

    private void Update()
    {
        Time.timeScale = timeScale;
        transform.position += Vector3.forward * (Time.deltaTime * speedMoving);
        PcControl();
        if (CurrentState == State.Stay)
        {
            speedMoving = 0;
            CurrentState = State.Stay;
        }
        if (CurrentState == State.Moving)
        {
            speedMoving = 5;
            CurrentState = State.Moving;
        }
    }

    public void SetState(State currentState)
    {
        CurrentState = currentState;
    }
    private void PcControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _touchLastPos = camera.ScreenToViewportPoint(Input.mousePosition);
        }
        
        if (Input.GetMouseButton(0))
        {       
            Vector3 delta = camera.ScreenToViewportPoint(Input.mousePosition) - _touchLastPos;
            transform.position = new Vector3(delta.x * sensity, 0, transform.position.z);
        }
    }

}
