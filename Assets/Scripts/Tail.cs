using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public float AnimationDuration;
    [SerializeField] private int _startCountCircles;
    [SerializeField] private float _circleDiameter;
    [SerializeField] private Transform _snakeCell;
    [SerializeField] private Transform _snakeHead;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private TextMeshPro _tailSizeCellTextMP;

    private int _numberOfCells;
    private float _distance;
    private Transform _removableCell;
    [SerializeField] private Block _currentBlock;
    [SerializeField] private List<Transform> _snakeCircles = new List<Transform>();
    [SerializeField] private List<Vector3> _positions = new List<Vector3>();

    private void Awake()
    {
        _positions.Add(_snakeHead.position);
    }

    private void Start()
    {
        AddCircle(_startCountCircles);
    }

    private void Update()
    {
        _distance = (_snakeHead.position - _positions[0]).magnitude;
        if (_distance > _circleDiameter)
            Position();
        if (_playerMovement.CurrentPlayerState == PlayerState.Moving)
        {
            MovingCircles();
        }
    }

    public void SelectBlock(Block block)
    {
        _currentBlock = block;
    }

    private void Position()
    {
        Vector3 direction = (_snakeHead.position - _positions[0]).normalized;
        Vector3 position = _positions[0] + direction * _circleDiameter;
        _positions.Insert(0, position);
        _positions.RemoveAt(_positions.Count - 1);
        _distance -= _circleDiameter;
    }

    private void MovingCircles()
    {
        for (int i = 0; i < _snakeCircles.Count; i++)
        {
            _snakeCircles[i].position = Vector3.Lerp(_positions[i + 1], _positions[i], _distance / _circleDiameter);
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
            Transform circle = Instantiate(_snakeCell, _positions[^1],
                setRotation, transform);
            _snakeCircles.Add(circle);
            _positions.Add(circle.position);
            UpdateSizeCellText();
        }
    }

    private void UpdateSizeCellText()
    {
        _tailSizeCellTextMP.text = _snakeCircles.Count.ToString();
    }
    
    public void RemoveCell(int countRemoveCell, Vector3 targetPosition)
    {
        Sequence sequence = DOTween.Sequence();
        
        for (int i = 0; i < countRemoveCell; i++)
        {
            sequence.Append(_snakeCircles[i].DOJump(targetPosition, 2, 1,AnimationDuration));
            sequence.AppendCallback(DeleteCell);
            sequence.AppendCallback(UpdateSizeCellText);
            sequence.AppendInterval(AnimationDuration);
        }
    }
    
    private void DeleteCell()
    {
        _removableCell = _snakeCircles[0];
        Invoke("DestroyCell", AnimationDuration);
        _currentBlock.SubtractBlockCount();
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
