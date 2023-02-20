using System;
using SaveLoadSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class SoundsEffects : MonoBehaviour
    {
        [SerializeField] private SettingsSaveData _settingsSaveData;
        [SerializeField] private Slider _soundValeuSlider;
        // 0 - jump, 1 - openMenu, 2 - pickUpLoot, 3 - deleteBasket, 4 - lose, 5 - start, 6 - finish
        [SerializeField] private AudioSource[] _soundEffects;
        [SerializeField] private AudioSource _jumpInBasket;
        [SerializeField] private AudioSource _openMenu;

        private float _soundValue = 0.5f;
        
        private void Start()
        {
            if (!_settingsSaveData.IsCreatedSoundSave)
            {
                _settingsSaveData.SoundWriteData(_soundValue);
                _settingsSaveData.CreateSoundSave();
            }
            else
            {
                _settingsSaveData.SoundReadData(out _soundValue);
            }
    
            SetSoundValue(_soundValue);
            _soundValeuSlider.value = _soundValue;
        }

        /// <param name="0">Jump</param> <param name="1">OpenMenu</param>
        /// <param name="2">PickUpLoot</param> <param name="3">DeleteBasket</param>
        /// <param name="4">Lose</param> <param name="5">Start</param>
        /// <param name="6">Finish</param>
        public void PlaySoundEffect(int index)
        {
            _soundEffects[index].Play();
        }
        
        public void SetSoundValue(float value)
        {
            _soundValue = value;
            _settingsSaveData.SoundWriteData(_soundValue);
            for (int i = 0; i < _soundEffects.Length; i++)
            {
                _soundEffects[i].volume = Mathf.Lerp(0f, 0.5f, _soundValue);
            }
        }
    }
}