using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    [SerializeField] private TextMeshPro _textBlockCount;
    [SerializeField] private Renderer _renderer;
    
    private PlayerMovement _playerMovement;
    
    public int BlockCount { get; private set; }
    public bool IsActive { get; private set; } = false;
    
    private void Start()
    {
        UpdateProperties();
    }

    public void SetBlockCount(int blockCount)
    {
        BlockCount = blockCount;
    }
    public void SubtractBlockCount()
    {
        BlockCount -= 1;
        UpdateProperties();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!this.enabled) return;
        if (collision.collider.TryGetComponent(out PlayerMovement playerMovement))
        {
            DisabledBlocks();
            _playerMovement = playerMovement;
            _playerMovement.SetState(PlayerState.Stay);
            TailManagment tailManagment = collision.collider.GetComponentInChildren<TailManagment>();
            
            DeleteTailCell(tailManagment);
        }
    }

    private void DeleteTailCell(TailManagment tailManagment)
    {
        tailManagment.SelectBlock(this);
        tailManagment.NormalizePosition();
        tailManagment.RemoveCell(BlockCount, _renderer.transform.position);
    }

    private void DisabledBlocks()
    {
        RowBlockGenerator rowBlockGenerator = GetComponentInParent<RowBlockGenerator>();
        IsActive = true;
        rowBlockGenerator.DisabledBlock();
    }

    private void CalculateColor()
    {
        switch (BlockCount)
        {
            case < 11:
                SetColor(_colors[0], _colors[1], BlockCount);
                break;
            case < 21:
                SetColor(_colors[1], _colors[2], BlockCount - 10);
                break;
            case < 31:
                SetColor(_colors[2], _colors[3], BlockCount - 20);
                break;
            case < 41:
                SetColor(_colors[3], _colors[4], BlockCount - 30);
                break;
            case < 51:
                SetColor(_colors[4], _colors[5], BlockCount - 40);
                break;
        }
    }

    private void SetColor(Color color1, Color color2, float value)
    {
        float mediumColor = Mathf.InverseLerp(0f, 10f, value);
        _renderer.material.color = Color.Lerp(color1, color2, mediumColor);
    }
    
    private void OnDestroy()
    {
        if (_playerMovement && _playerMovement.CurrentPlayerState == PlayerState.Stay)
            _playerMovement.SetState(PlayerState.MovingInGame);
    }

    private void UpdateTextValue()
    {
        _textBlockCount.text = BlockCount.ToString();
    }
    
    private void UpdateProperties()
    {
        UpdateTextValue();
        CalculateColor();
    }
}
