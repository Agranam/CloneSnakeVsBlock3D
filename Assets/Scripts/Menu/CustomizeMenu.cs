using SaveLoadSystem;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class CustomizeMenu : MonoBehaviour
    {
        public int CurrentPlaygroundMaterial { get; private set; }
        public int CurrentPlayerColor { get; private set; }
        [SerializeField] private MaterialsList _payerMaterialsList;
        [SerializeField] private SkinsSaveData _skinsSaveData;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Material[] _playerMaterials;
        [SerializeField] private Toggle[] _setToggleSkins;
        [SerializeField] private Toggle[] _setTogglePlaygrounds;

        private Floor[] _floors;
        private Wall[] _walls;
        private int _emissionColor = Shader.PropertyToID("_EmissionColor");

        private void Start()
        {
            LoadSkinsSaveData();
            _setToggleSkins[CurrentPlayerColor].isOn = true;
            _setTogglePlaygrounds[CurrentPlaygroundMaterial].isOn = true;
            SetPlayground(CurrentPlaygroundMaterial);
            SetSkin(CurrentPlayerColor);
        }

        public void SetSkin(int currentIndex)
        {
            for (int i = 0; i < _playerMaterials.Length; i++)
            {
                _playerMaterials[i].color = _payerMaterialsList.playerColors[currentIndex];
            }
            _playerMaterials[2].SetColor(_emissionColor, _payerMaterialsList.playerColors[currentIndex]);
            CurrentPlayerColor = currentIndex;
            _skinsSaveData.CurrentSkinWriteData(CurrentPlayerColor);
        }
        
        public void SetPlayground(int currentIndex)
        {
            _floors = FindObjectsOfType<Floor>();
            _walls = FindObjectsOfType<Wall>();
            foreach (var floor in _floors)
            {
                floor.GetComponent<Renderer>().material = _payerMaterialsList.floorMaterials[currentIndex];
            }
            foreach (var wall in _walls)
            {
                wall.GetComponent<Renderer>().material = _payerMaterialsList.wallMaterials[currentIndex];
            }
            CurrentPlaygroundMaterial = currentIndex;
            _backgroundImage.sprite = _payerMaterialsList.backgroundImages[CurrentPlaygroundMaterial];
            _skinsSaveData.CurrentPlaygroundWriteData(CurrentPlaygroundMaterial);
        }

        private void LoadSkinsSaveData()
        {
            _skinsSaveData.CurrentSkinReadData(out int currentPlayerColor);
            CurrentPlayerColor = currentPlayerColor;
            _skinsSaveData.CurrentPlaygroundReadData(out int currentPlaygroundMaterial);
            CurrentPlaygroundMaterial = currentPlaygroundMaterial;
        }
    }
}