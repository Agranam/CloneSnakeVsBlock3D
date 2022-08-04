using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int BlockCount;
    [SerializeField] private PlayerMovement playerMovement;
    
    void Start()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponentInChildren<Tail>())
        {
            Debug.Log("OnCollision");
            Tail tail = collision.collider.GetComponentInChildren<Tail>();
            playerMovement.SetState(State.Stay);
            tail.NormalizePosition();
            tail.RemoveCircle(BlockCount);
            Invoke("DestroyBlock", BlockCount * 0.25f);
        }
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        playerMovement.SetState(State.Moving);

    }
}
