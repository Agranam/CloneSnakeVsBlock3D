using TMPro;
using UnityEngine;

public class TailValue : MonoBehaviour
{
    [SerializeField] private TextMeshPro _tailSizeCellTextMP;
    [SerializeField] private TextMeshProUGUI _currentNumberOfCells;

    public void TextEnabled(bool isActive)
    {
        _tailSizeCellTextMP.gameObject.SetActive(isActive);
    }
    
    public void UpdateText(int numberOfCells)
    {
        _currentNumberOfCells.text = numberOfCells.ToString();
        _tailSizeCellTextMP.text = numberOfCells.ToString();
    }
}
