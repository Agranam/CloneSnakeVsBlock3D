using System;
using SaveLoadSystem;
using UnityEngine;

namespace Menu
{
    public class PlaygroundsMenu : MonoBehaviour
    {
        public static int CurrentPlaygroundMaterial;
        [SerializeField] private SkinsSaveData _skinsSaveData;

        [SerializeField] private Renderer[] _playgroundParts;

        private void Start()
        {
            LoadPlaygroundsSaveData();
        }

        private void SetPlayground(int currentIndex)
        {
            
        }

        private void LoadPlaygroundsSaveData()
        {
            _skinsSaveData.CurrentPlaygroundReadData(out CurrentPlaygroundMaterial);
        }
    }
}