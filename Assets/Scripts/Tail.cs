using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField] private int startCountCircles;
    [SerializeField] private float circleDiameter;
    [SerializeField] private Transform snakeHead;
    [SerializeField] private PlayerMovement playerMovement;

    private int _cellsCount;
    private float _distance;
    [SerializeField] private List<Transform> _snakeCircles = new List<Transform>();
    [SerializeField] private List<Vector3> _positions = new List<Vector3>();

    private void Awake()
    {
        _positions.Add(snakeHead.position);
    }

    private void Start()
    {
        AddCircle(startCountCircles);
    }

    private void Update()
    {
        _distance = (snakeHead.position - _positions[0]).magnitude;
        if (_distance > circleDiameter)
            Position();
        if (playerMovement.CurrentState == State.Moving)
        {
            MovingCircles();
        }
    }

    private void Position()
    {
        Vector3 direction = (snakeHead.position - _positions[0]).normalized;
        Vector3 position = _positions[0] + direction * circleDiameter;
        _positions.Insert(0, position);
        _positions.RemoveAt(_positions.Count - 1);
        _distance -= circleDiameter;
    }

    private void MovingCircles()
    {
        for (int i = 0; i < _snakeCircles.Count; i++)
        {
            _snakeCircles[i].position = Vector3.Lerp(_positions[i + 1], _positions[i], _distance / circleDiameter);
        }
    }
    public void NormalizePosition()
    {
        for (int i = 1; i < _positions.Count; i++)  
        {
            _positions[i] = _snakeCircles[i - 1].position;
        }
        _distance = 0;
    }

    public void AddCircle(int countCircles)
    {
        for (int i = 0; i < countCircles; i++)
        {
            Transform circle = Instantiate(snakeHead, _positions[^1],
                Quaternion.identity, transform);
            _snakeCircles.Add(circle);
            _positions.Add(circle.position);
        }
    }

    public void RemoveCircle(int countRemoveCircles)
    {
        StartCoroutine(WaitingEndOfMoving(countRemoveCircles));
    }

    private IEnumerator WaitingEndOfMoving(int count)
    {
        for (int a = 0; a < count; a++)
        {
            DeleteCircle();

            yield return StartCoroutine(MovementCycle());
        }

        _distance = circleDiameter;
    }

    private void DeleteCircle()
    {
        Destroy(_snakeCircles[0].gameObject);
        Position();
        _snakeCircles.RemoveAt(0);
        _positions.RemoveAt(1);
    }

    private IEnumerator MovementCycle()
    {
        for (int i = 0; i < _snakeCircles.Count; i++)
        {
            StartCoroutine(SmoothMoving(i, 4f));
            
        }
        yield return new WaitForSeconds(0.25f);
    }

    private IEnumerator SmoothMoving(int index, float time)
    {
        for (float t = 1; t > 0; t -= Time.deltaTime * time)
        {
            Vector3 position = _positions[index + 1];
            _snakeCircles[index].position = Vector3.Lerp(position, position - Vector3.forward, t);
            yield return null;
        }
    }
}
