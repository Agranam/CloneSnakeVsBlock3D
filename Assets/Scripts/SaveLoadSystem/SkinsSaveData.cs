using Menu;
using UnityEngine;

namespace SaveLoadSystem
{
    public class SkinsSaveData : MonoBehaviour
    {
        [SerializeField] private SkinsShop _skinsShop;
        
        private SkinsData _mySkinsData = new SkinsData();

        public void SkinsWriteData()
        {
            _mySkinsData.OpenSkins = _skinsShop.OpenSkins;
            _mySkinsData.OpenPlaygrounds = _skinsShop.OpenPlaygrounds;
        }
        public void SkinsReadData()
        {
            _skinsShop.OpenSkins = _mySkinsData.OpenSkins;
            _skinsShop.OpenPlaygrounds = _mySkinsData.OpenPlaygrounds;
        }

        
        
    }
    
    [System.Serializable]
    public struct SkinsData
    {
        public bool[] OpenSkins;
        public bool[] OpenPlaygrounds;
    }
}