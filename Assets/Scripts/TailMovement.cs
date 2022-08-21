using System.Collections.Generic;
using DG.Tweening;
using Menu;
using SaveLoadSystem;
using UnityEngine;

public class TailMovement : MonoBehaviour
{
    public float AnimationDuration = 0.2f;
    public int NumberOfCells;
    [SerializeField] private PlayerSaveData _playerSaveData;
    [SerializeField] private Transform _snakeCellPrefab;
    [SerializeField] private Transform _snakeHead;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private TailValue _tailValue;
    [SerializeField] private SoundsEffects _soundsEffects;
    
    private readonly float _cellDiameter = 1;
    private float _distance;
    private Transform _removableCell;
    private Block _currentBlock;
    private List<Transform> _snakeCells = new List<Transform>();
    private List<Vector3> _positionsCells = new List<Vector3>();

    private void Awake()
    {
        _positionsCells.Add(_snakeHead.position);
        _playerSaveData.TailLenghtReadData(out NumberOfCells);
    }

    private void Start()
    {
        AddCell(NumberOfCells);
        UpdateTailLenght();
    }

    private void Update()
    {
        _distance = (_snakeHead.position - _positionsCells[0]).magnitude;
        if (_distance > _cellDiameter)
            Position();
        if (_playerMovement.CurrentPlayerState is PlayerState.MovingInGame or PlayerState.MovingInBackground)
        {
            MovingCircles();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _playerSaveData.TailLenghtWriteData(NumberOfCells);
        }
    }

    public void SelectBlock(Block block)
    {
        _currentBlock = block;
    }

    private void Position()
    {
        Vector3 direction = (_snakeHead.position - _positionsCells[0]).normalized;
        Vector3 position = _positionsCells[0] + direction * _cellDiameter;
        _positionsCells.Insert(0, position);
        _positionsCells.RemoveAt(_positionsCells.Count - 1);
        _distance -= _cellDiameter;
    }

    private void MovingCircles()
    {
        for (int i = 0; i < _snakeCells.Count; i++)
        {
            _snakeCells[i].position = Vector3.Lerp(_positionsCells[i + 1], _positionsCells[i], _distance / _cellDiameter);
        }
    }

    public void NormalizePosition()
    {
        for (int i = 1; i < _positionsCells.Count; i++)  
        {
            _positionsCells[i] = _snakeCells[i - 1].position;
        }
        _distance = 0;
    }

    public void AddCell(int numberOfCells)
    {
        for (int i = 0; i < numberOfCells; i++)
        {
            Quaternion setRotation = Quaternion.Euler
                (Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            Transform circle = Instantiate(_snakeCellPrefab, _positionsCells[^1],
                setRotation, transform);
            _snakeCells.Add(circle);
            _positionsCells.Add(circle.position);
            UpdateTailLenght();
        }
    }

    private void UpdateTailLenght()
    {
        NumberOfCells = _snakeCells.Count;
        _tailValue.UpdateText(NumberOfCells);
    }
    
    public void RemoveCell(int numberOfRemoveCell, Vector3 targetPosition)
    {
        Sequence sequence = DOTween.Sequence();
        
        for (int i = 0; i < numberOfRemoveCell; i++)
        {
            sequence.Append(_snakeCells[i].DOJump(targetPosition, 2, 1,AnimationDuration));
            sequence.AppendCallback(DeleteCell);
            sequence.AppendCallback(UpdateTailLenght);
            sequence.AppendInterval(AnimationDuration);
        }
    }
    
    private void DeleteCell()
    {
        _soundsEffects.Jump();
        _removableCell = _snakeCells[0];
        Invoke("DestroyCell", AnimationDuration);
        _currentBlock.SubtractBlockCount();
        _snakeCells.RemoveAt(0);
        _positionsCells.RemoveAt(_positionsCells.Count - 1);
        for (int i = 0; i < _snakeCells.Count; i++)
        {
            _snakeCells[i].DOMove(_positionsCells[i + 1], AnimationDuration, false);
        }
    }

    private void DestroyCell()
    {
        Destroy(_removableCell.gameObject);
    }
}
