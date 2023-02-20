using System;
using DG.Tweening;
using Menu;
using TMPro;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private int _minValue, _maxValue;
    [SerializeField] private Color _color1, _color2;
    [SerializeField] private TextMeshPro _textCellsCount;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _numberOfCells;
    [SerializeField] private GameObject _prefabFX;

    private Color _currentColor;
    private SoundsEffects _soundsEffects;
    private Tween _scaling;
    private void Start()
    {
        _soundsEffects = FindObjectOfType<SoundsEffects>();
        UpdateProperties();
        _scaling = transform.DOShakeScale(5, 0.2f, 1, 20, false).SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInChildren<TailManagment>())
        {
            TailManagment tailManagment = other.GetComponentInChildren<TailManagment>();
            tailManagment.AddCell(_numberOfCells);
            _soundsEffects.PlaySoundEffect(2);
            _scaling.Kill();
            GameObject effect = Instantiate(_prefabFX, _renderer.transform.position, Quaternion.identity);
            effect.GetComponent<Renderer>().material.color = _currentColor;
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
        _currentColor = Color.Lerp(color1, color2, mediumColor);
        _renderer.material.color = _currentColor;
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }
    
    private void UpdateTextValue()
    {
        _textCellsCount.text = _numberOfCells.ToString();
    }
    
    private void UpdateProperties()
    {
        UpdateTextValue();
        CalculateColor();
    }

    private void OnDestroy()
    {
        _scaling.Kill();
    }
}
