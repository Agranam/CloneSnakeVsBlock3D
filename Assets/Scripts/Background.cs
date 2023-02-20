using SaveLoadSystem;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private MaterialsList _materialsList;
    [SerializeField] private Image _backgroundImage;
    
    private void Start()
    {
        _backgroundImage.sprite = _materialsList.backgroundImages[SkinsSaveData.CurrentPlaygroundMaterial];
    }
}