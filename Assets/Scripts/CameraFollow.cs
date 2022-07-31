using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speedMoving = 2f;

    void Update()
    {
        transform.position = Vector3.Lerp(_target.position, transform.position, _speedMoving * Time.deltaTime);
    }
}
