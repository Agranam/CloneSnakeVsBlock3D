using TMPro;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private int _minValue, _maxValue;
    [SerializeField] private Color _color1, _color2;
    [SerializeField] private TextMeshPro _textCellsCount;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _numberOfCells;

    private void Start()
    {
        UpdateProperties();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInChildren<Tail>())
        {
            Tail tail = other.GetComponentInChildren<Tail>();
            tail.AddCircle(_numberOfCells);
            DestroyBlock();
        }
    }
    public void SetNumberOfCells(int numberOfCells)
    {
        _numberOfCells = numberOfCells;
    }

    private void CalculateColor()
    {
        SetColor(_color1, _color2, _minValue, _maxValue);
    }

    private void SetColor(Color color1, Color color2, int value1, int value2)
    {
        float mediumColor = Mathf.InverseLerp(value1, value2, _numberOfCells);
        _renderer.material.color = Color.Lerp(color1, color2, mediumColor);
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }
    
    private void UpdateTextValue()
    {
        _textCellsCount.text = _numberOfCells.ToString();
    }
    
    [ContextMenu("Update properties")]
    private void UpdateProperties()
    {
        UpdateTextValue();
        CalculateColor();
    }
}
