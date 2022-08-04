using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Tail tail;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            tail.AddCircle(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //tail.RemoveCircle();
        }
    }
}
