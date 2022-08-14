using System;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int _blockCount;
    [SerializeField] private Color[] _colors;
    [SerializeField] private TextMeshPro _textBlockCount;
    [SerializeField] private Renderer _renderer;
    
    [SerializeField] private PlayerMovement _playerMovement;

    private void Start()
    {
        UpdateProperties();
    }

    public void SetBlockCount(int blockCount)
    {
        _blockCount = blockCount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out PlayerMovement playerMovement))
        {
            _playerMovement = playerMovement;
            _playerMovement.SetState(PlayerState.Stay);
            Tail tail = collision.collider.GetComponentInChildren<Tail>();
            tail.NormalizePosition();
            tail.RemoveCell(_blockCount, _renderer.transform.position);
            float delay = tail.AnimationDuration * 2;
            Invoke("DestroyBlock", _blockCount * delay);
        }
    }
    
    private void CalculateColor()
    {
        switch (_blockCount)
        {
            case < 11:
                SetColor(_colors[0], _colors[1], _blockCount);
                break;
            case < 21:
                SetColor(_colors[1], _colors[2], _blockCount - 10);
                break;
            case < 31:
                SetColor(_colors[2], _colors[3], _blockCount - 20);
                break;
            case < 41:
                SetColor(_colors[3], _colors[4], _blockCount - 30);
                break;
            case < 51:
                SetColor(_colors[4], _colors[5], _blockCount - 40);
                break;
        }
    }

    private void SetColor(Color color1, Color color2, float value)
    {
        float mediumColor = Mathf.InverseLerp(0f, 10f, value);
        _renderer.material.color = Color.Lerp(color1, color2, mediumColor);
    }
    
    private void DestroyBlock()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (_playerMovement)
            _playerMovement.SetState(PlayerState.Moving);
    }

    private void UpdateTextValue()
    {
        _textBlockCount.text = _blockCount.ToString();
    }
    
    [ContextMenu("Update properties")]
    private void UpdateProperties()
    {
        UpdateTextValue();
        CalculateColor();
    }
}
