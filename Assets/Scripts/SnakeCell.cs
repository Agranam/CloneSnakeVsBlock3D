using DG.Tweening;
using Menu;
using SaveLoadSystem;
using ScriptableObjects;
using UnityEngine;

public class SnakeCell : MonoBehaviour
{
    [SerializeField] private Renderer _snakeCell;
    [SerializeField] private MaterialsList _payerMaterialsList;
    [SerializeField] private float _duration = 1;
    
    private int _currentPlayerSkin;

    private void Start()
    {
        int currentPlayerSkin = SkinsMenu.CurrentPlayerColor;

        _snakeCell.material.color = _payerMaterialsList.playerColors[currentPlayerSkin];
        
        transform.DOLocalRotate(new Vector3(0, 360, 0), _duration).
            SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetLink(gameObject);
    }
}
