using UnityEngine;

namespace SaveLoadSystem
{
    public class SkinsSaveData : MonoBehaviour
    {
        
        private SkinsData _mySkinsData = new SkinsData();
        public bool IsCreatedSave { get; private set; }
        public static int CurrentPlaygroundMaterial { get; private set; }

        private void Awake()
        {
            VerifyRead();
            CurrentPlaygroundReadData(out int currentPlaygroundMaterial);
            CurrentPlaygroundMaterial = currentPlaygroundMaterial;
        }

        public void OpenSkinsWriteData(bool[] openSkins)
        {
            _mySkinsData.OpenSkins = openSkins;
            SaveSave();
        }
        public void OpenSkinsReadData(out bool[] openSkins)
        {
            LoadSave();
            openSkins = _mySkinsData.OpenSkins;
        }
        public void OpenPlaygroundsWriteData(bool[] openPlaygrounds)
        {
            _mySkinsData.OpenPlaygrounds = openPlaygrounds;
            SaveSave();
        }
        public void OpenPlaygroundsReadData(out bool[] openPlaygrounds)
        {
            LoadSave();
            openPlaygrounds = _mySkinsData.OpenPlaygrounds;
        }
        public void PriceSkinsWriteData(int[] priceSkins)
        {
            _mySkinsData.PriceSkins = priceSkins;
            SaveSave();
        }
        public void PriceSkinsReadData(out int[] priceSkins)
        {
            LoadSave();
            priceSkins = _mySkinsData.PriceSkins;
        }
        public void PricePlaygroundsWriteData(int[] pricePlaygrounds)
        {
            _mySkinsData.PricePlaygrounds = pricePlaygrounds;
            SaveSave();
        }
        public void PricePlaygroundsReadData(out int[] pricePlaygrounds)
        {
            LoadSave();
            pricePlaygrounds = _mySkinsData.PricePlaygrounds;
        }
        public void CurrentSkinWriteData(int currentSkin)
        {
            _mySkinsData.CurrentSkin = currentSkin;
            SaveSave();
        }
        public void CurrentSkinReadData(out int currentSkin)
        {
            LoadSave();
            currentSkin = _mySkinsData.CurrentSkin;
        }
        public void CurrentPlaygroundWriteData(int currentPlayground)
        {
            _mySkinsData.CurrentPlayground = currentPlayground;
            CurrentPlaygroundMaterial = currentPlayground;
            SaveSave();
        }
        public void CurrentPlaygroundReadData(out int currentPlayground)
        {
            LoadSave();
            currentPlayground = _mySkinsData.CurrentPlayground;
        }
        public void VerifyRead()
        {
            LoadSave();
            IsCreatedSave = _mySkinsData.IsCreatedSave;
        }
        public void CreatedSave()
        {
            _mySkinsData.IsCreatedSave = true;
            SaveSave();
        }
        private void SaveSave()
        {
            SaveGameManager.CurrentSaveData.SkinsData = _mySkinsData;
            SaveGameManager.SaveGame();
        }
        private void LoadSave()
        {
            SaveGameManager.LoadGame();
            _mySkinsData = SaveGameManager.CurrentSaveData.SkinsData;
        }
    }
    
    [System.Serializable]
    public struct SkinsData
    {
        public bool IsCreatedSave;
        public bool[] OpenSkins;
        public bool[] OpenPlaygrounds;
        public int[] PriceSkins;
        public int[] PricePlaygrounds;
        public int CurrentSkin;
        public int CurrentPlayground;
    }
}