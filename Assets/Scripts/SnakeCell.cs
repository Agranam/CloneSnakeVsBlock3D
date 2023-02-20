using UnityEngine;

public class SnakeCell : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(180 * Time.deltaTime, 0f, 0f);
    }
}
