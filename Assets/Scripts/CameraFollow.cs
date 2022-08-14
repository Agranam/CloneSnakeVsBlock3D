using UnityEngine;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speedMoving = 2f;

    void Update()
    {
        Vector3 transformPosition = Vector3.Lerp(target.position, transform.position, speedMoving * Time.deltaTime);
        transform.position = new Vector3(0f, transformPosition.y, transformPosition.z);
    }
}
