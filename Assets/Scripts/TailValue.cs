using TMPro;
using UnityEngine;

public class TailValue : MonoBehaviour
{
    [SerializeField] private TextMeshPro _tailSizeCellTextMP;

    public void TextEnabled(bool isActive)
    {
        _tailSizeCellTextMP.gameObject.SetActive(isActive);
    }
    
    public void UpdateText(int numberOfCells)
    {
        _tailSizeCellTextMP.text = numberOfCells.ToString();
    }
}
