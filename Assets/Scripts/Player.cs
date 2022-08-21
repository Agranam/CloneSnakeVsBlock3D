using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private TailMovement tailMovement;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            tailMovement.AddCell(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //tail.RemoveCircle();
        }
    }
}
