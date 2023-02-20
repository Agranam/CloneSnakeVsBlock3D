using SaveLoadSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class OtherSettings : MonoBehaviour
    {
        [SerializeField] private SettingsSaveData _settingsSaveData;
        [SerializeField] private Light _mainLight;
        [SerializeField] private Slider _brightnessValeuSlider;
        [SerializeField] private float _brightnessValue = 0.5f;
        
        private void Start()
        {
            if (!_settingsSaveData.IsCreatedBrightnessSave)
            {
                _settingsSaveData.BrightnessWriteData(_brightnessValue);
                _settingsSaveData.CreateBrightnessSave();
            }
            else
            {
                _settingsSaveData.BrightnessReadData(out _brightnessValue);
            }
    
            SetBrightnessValue(_brightnessValue);
            _brightnessValeuSlider.value = _brightnessValue;
        }

        public void SetBrightnessValue(float value)
        {
            _brightnessValue = value;
            _settingsSaveData.BrightnessWriteData(_brightnessValue);
            _mainLight.intensity = Mathf.Lerp(0f, 5f, _brightnessValue);
        }
    }
}