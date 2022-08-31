using UnityEngine;

namespace SaveLoadSystem
{
    public class SettingsSaveData : MonoBehaviour
    {
        private SettingsData _mySettingsData = new SettingsData();
        public bool IsCreatedMusicSave { get; private set; }
        public bool IsCreatedSoundSave { get; private set; }
        public bool IsCreatedBrightnessSave { get; private set; }

        private void Awake()
        {
            VerifyMusicSave();
            VerifySoundSave();
            VerifyBrightnessSave();
        }

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
        public void BrightnessWriteData(float brightnessValue)
        {
            _mySettingsData.Brightness = brightnessValue;
            SaveSave();
        }
        public void BrightnessReadData(out float brightnessValue)
        {
            LoadSave();
            brightnessValue = _mySettingsData.Brightness;
        }
        private void VerifyMusicSave()
        {
            LoadSave();
            IsCreatedMusicSave = _mySettingsData.IsMusicSave;
        }
        public void CreateMusicSave()
        {
            _mySettingsData.IsMusicSave = true;
            SaveSave();
        }
        private void VerifySoundSave()
        {
            LoadSave();
            IsCreatedSoundSave = _mySettingsData.IsSoundSave;
        }
        public void CreateSoundSave()
        {
            _mySettingsData.IsSoundSave = true;
            SaveSave();
        }
        private void VerifyBrightnessSave()
        {
            LoadSave();
            IsCreatedBrightnessSave = _mySettingsData.IsBrightnessSave;
        }
        public void CreateBrightnessSave()
        {
            _mySettingsData.IsBrightnessSave = true;
            SaveSave();
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
        public bool IsMusicSave;
        public bool IsSoundSave;
        public bool IsBrightnessSave;
        public float MusicValue;
        public float SoundValue;
        public float Brightness;
    }
}