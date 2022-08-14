using System;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private float _speedMove = 5f;
    [SerializeField] private float _speedDrag = 10f;
    [SerializeField] private Camera _mainCamera;
    private Vector3 _dragOffset;

    private void Update()
    {
        transform.position += Vector3.forward * (Time.deltaTime * _speedMove);
        
        if (Input.GetMouseButtonDown(0))
        {
            _dragOffset = transform.position - GetMousePos();
        }
        
        if (Input.GetMouseButton(0))
        {       
            transform.position = Vector3.MoveTowards(transform.position, 
                GetMousePos() + _dragOffset, _speedDrag * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        _dragOffset = transform.position - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards(transform.position, 
                GetMousePos() + _dragOffset, _speedDrag * Time.deltaTime);
    }

    Vector3 GetMousePos()
    {
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = 0;
        return mousePos;
    }


}
