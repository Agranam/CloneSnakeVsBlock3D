using System.Collections.Generic;
using DG.Tweening;
using Menu;
using SaveLoadSystem;
using UnityEngine;

public class TailManagment : MonoBehaviour
{
    public int NumberOfCells { get; private set; }
    [SerializeField] private float _durationAnimation = 2f;
    [SerializeField] private PlayerSaveData _playerSaveData;
    [SerializeField] private Transform _snakeCellPrefab;
    [SerializeField] private Transform _snakeHead;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private TailValue _tailValue;
    [SerializeField] private SoundsEffects _soundsEffects;
    [SerializeField] private GameManager _gameManager;

    private bool _isDied = false;
    private float _cellDiameter = 1;
    private float _distance;
    private float _currentDurationAnimation;
    private Transform _removableCell;
    private Block _currentBlock;
    private List<Transform> _snakeCells = new List<Transform>();
    private List<Vector3> _positionsCells = new List<Vector3>();

    private void Awake()
    {
        _positionsCells.Add(_snakeHead.position);
        _playerSaveData.TailLenghtReadData(out int numberOfCells);
        NumberOfCells = numberOfCells;
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

        if (Input.GetKeyDown(KeyCode.D))
        {
            FastDeleteCell();
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
        //playerSaveData.TailLenghtWriteData(NumberOfCells);
        _tailValue.UpdateText(NumberOfCells);
        if (NumberOfCells == 0 && _isDied)
        {
            _playerMovement.SetState(PlayerState.Died);
            _gameManager.LevelLost();
            _isDied = false;
        }
    }
    
    public void RemoveCell(int numberOfRemoveCell, Vector3 targetPosition)
    {
        
        int cellsCount;
        if (numberOfRemoveCell > _snakeCells.Count)
        {
            cellsCount = _snakeCells.Count;
            _isDied = true;
            if (_snakeCells.Count == 0)
                UpdateTailLenght();
        }
        else
        {
            cellsCount = numberOfRemoveCell;
        }

        if (NumberOfCells > 20)
        {
            DOTween.SetTweensCapacity(400, 100);
        }

        CalculateDurationAnimation(cellsCount);
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < cellsCount; i++)
        {
            sequence.Append(_snakeCells[i].DOJump(targetPosition, 2, 1,_currentDurationAnimation));
            sequence.AppendCallback(DeleteCell);
            sequence.AppendCallback(UpdateTailLenght);
            sequence.AppendInterval(_currentDurationAnimation);
        }
    }

    private void CalculateDurationAnimation(float numberOfBlock)
    {
        float t = Mathf.Abs((numberOfBlock / 50) - 1);
        _currentDurationAnimation = Mathf.Lerp(0.03f, _durationAnimation, t);
    }
    
    private void DeleteCell()
    {
        _soundsEffects.PlaySoundEffect(0);
        _removableCell = _snakeCells[0];
        Invoke(nameof(DestroyCell), _currentDurationAnimation);
        _currentBlock.SubtractBlockCount();
        _snakeCells.RemoveAt(0);
        _positionsCells.RemoveAt(_positionsCells.Count - 1);
        for (int i = 0; i < _snakeCells.Count; i++)
        {
            _snakeCells[i].DOMove(_positionsCells[i + 1], _currentDurationAnimation, false);
        }

        if (_currentBlock.BlockCount == 0)
        {
            Invoke(nameof(DestroyBlock), _currentDurationAnimation);
        }
    }

    public void FastDeleteCell()
    {
        Destroy(_snakeCells[0].gameObject);
        _snakeCells.RemoveAt(0);
        _positionsCells.RemoveAt(_positionsCells.Count - 1);
        UpdateTailLenght();
        _playerSaveData.TailLenghtWriteData(NumberOfCells);
    }
    
    private void DestroyBlock()
    {
        Destroy(_currentBlock.gameObject);
    }

    private void DestroyCell()
    {
        Destroy(_removableCell.gameObject);
    }
}
