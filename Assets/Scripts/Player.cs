using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Tail _tail;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _tail.AddCircle(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //tail.RemoveCircle();
        }
    }
}
