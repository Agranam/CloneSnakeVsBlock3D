using Menu;
using SaveLoadSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CustomizeMenu _customizeMenu;
    [SerializeField] private SkinsSaveData _skinsSaveData;
    
    private void Start()
    {
        _skinsSaveData.CurrentSkinReadData(out int currentPlayerColor);
        _customizeMenu.SetSkin(currentPlayerColor);
    }
}
