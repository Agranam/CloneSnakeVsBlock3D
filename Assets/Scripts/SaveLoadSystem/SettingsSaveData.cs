using UnityEngine;

namespace SaveLoadSystem
{
    public class SettingsSaveData : MonoBehaviour
    {
        private SettingsData _mySettingsData = new SettingsData();
        
        public void MusicWriteData(float musicValue)
        {
            _mySettingsData.MusicValue = musicValue;
            SaveSave();
        }
        public void MusicReadData(out float musicValue)
        {
            LoadSave();
            musicValue = _mySettingsData.MusicValue;
        }
        public void SoundWriteData(float soundValue)
        {
            _mySettingsData.SoundValue = soundValue;
            SaveSave();
        }
        public void SoundReadData(out float soundValue)
        {
            LoadSave();
            soundValue = _mySettingsData.SoundValue;
        }
        public void AnimationWriteData(bool isAnimation)
        {
            _mySettingsData.AnimationDisabled = isAnimation;
            SaveSave();
        }
        public void AnimationReadData(out bool isAnimation)
        {
            LoadSave();
            isAnimation = _mySettingsData.AnimationDisabled;
        }
        private void SaveSave()
        {
            SaveGameManager.CurrentSaveData.SettingsData = _mySettingsData;
            SaveGameManager.SaveGame();
        }
        private void LoadSave()
        {
            SaveGameManager.LoadGame();
            _mySettingsData = SaveGameManager.CurrentSaveData.SettingsData;
        }
    }
    
    [System.Serializable]
    public struct SettingsData
    {
        public float MusicValue;
        public float SoundValue;
        public bool AnimationDisabled;
    }
}