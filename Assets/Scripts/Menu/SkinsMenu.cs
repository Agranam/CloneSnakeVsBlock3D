using SaveLoadSystem;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class SkinsMenu : MonoBehaviour
    {
        public static int CurrentPlayerColor;
        [SerializeField] private MaterialsList _payerMaterialsList;
        [SerializeField] private SkinsSaveData _skinsSaveData;
        [SerializeField] private Renderer[] _playerHead;
        [SerializeField] private GameObject _tail;
        [SerializeField] private Toggle[] _setToggle;

        private Renderer[] _tailCells;

        private void Start()
        {
            LoadSkinsSaveData();
            _setToggle[CurrentPlayerColor].isOn = true;
            SetSkin(CurrentPlayerColor);
        }


        public void SetSkin(int currentIndex)
        {
            _tailCells = _tail.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < _playerHead.Length; i++)
            {
                _playerHead[i].material.color = _payerMaterialsList.playerColors[currentIndex];
            }
            for (int i = 0; i < _tailCells.Length; i++)
            {
                _tailCells[i].material.color = _payerMaterialsList.playerColors[currentIndex];
            }
            _skinsSaveData.CurrentSkinWriteData(currentIndex);
        }
        
        
        private void LoadSkinsSaveData()
        {
            _skinsSaveData.CurrentSkinReadData(out CurrentPlayerColor);
        }
    }
}