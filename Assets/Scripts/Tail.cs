using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public float AnimationDuration;
    [SerializeField] private int startCountCircles;
    [SerializeField] private float circleDiameter;
    [SerializeField] private Transform snakeHead;
    [SerializeField] private PlayerMovement playerMovement;

    private int _cellsCount;
    private float _distance;
    private Transform _removableCell;
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
        if (playerMovement.CurrentPlayerState == PlayerState.Moving)
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
            Quaternion setRotation = Quaternion.Euler
                (Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            Transform circle = Instantiate(snakeHead, _positions[^1],
                setRotation, transform);
            _snakeCircles.Add(circle);
            _positions.Add(circle.position);
        }
    }

    public void RemoveCell(int countRemoveCell, Vector3 targetPosition)
    {
        Sequence sequence = DOTween.Sequence();
        
        for (int i = 0; i < countRemoveCell; i++)
        {
            sequence.Append(_snakeCircles[i].DOJump(targetPosition, 2, 1,AnimationDuration));
            sequence.AppendCallback(DeleteCell);
            sequence.AppendInterval(AnimationDuration);
        }
    }
    
    private void DeleteCell()
    {
        _removableCell = _snakeCircles[0];
        Invoke("DestroyCell", AnimationDuration);

        _snakeCircles.RemoveAt(0);
        _positions.RemoveAt(_positions.Count - 1);
        for (int i = 0; i < _snakeCircles.Count; i++)
        {
            _snakeCircles[i].DOMove(_positions[i + 1], AnimationDuration, false);
        }
    }

    private void DestroyCell()
    {
        Destroy(_removableCell.gameObject);
    }
}
