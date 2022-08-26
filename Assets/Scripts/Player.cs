using Menu;
using ScriptableObjects;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private TailManagment _tailManagment;
    [SerializeField] private Renderer[] _playerRenderers;
    [SerializeField] private MaterialsList _payerMaterialsList;

    private Renderer[] _tailCells;
    
    private void Start()
    {
        int currentPlayerSkin = SkinsMenu.CurrentPlayerColor;
        for (int i = 0; i < _playerRenderers.Length; i++)
        {
            _playerRenderers[i].material.color = _payerMaterialsList.playerColors[currentPlayerSkin];
        }
        _tailCells = _tailManagment.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < _tailCells.Length; i++)
        {
            _tailCells[i].material.color = _payerMaterialsList.playerColors[currentPlayerSkin];
        }
    }
}
