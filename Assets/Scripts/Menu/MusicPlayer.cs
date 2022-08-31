using System.Collections.Generic;
using SaveLoadSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private Slider _musicValeuSlider;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private SettingsSaveData _settingsSaveData;
        [SerializeField] private AudioClip[] _gameMusic;
        private List<AudioClip> _playMusic = new List<AudioClip>();

        private float _musicValue = 0.5f;

        private void Awake()
        {
        }

        private void Start()
        {
            if (!_settingsSaveData.IsCreatedMusicSave)
            {
                _settingsSaveData.MusicWriteData(_musicValue);
                _settingsSaveData.CreateMusicSave();
            }
            else
            {
                _settingsSaveData.MusicReadData(out _musicValue);
            }

            addToList();
            SetMusicValue(_musicValue);
            _musicValeuSlider.value = _musicValue;
        }
        private void Update()
        {
            if (!_audio.isPlaying)
            {
                AudioClip playing = GetRandomClip(_playMusic.Count);
                _audio.clip = playing;
                _audio.Play();
                _playMusic.Remove(playing);
            }
            if (_playMusic.Count == 0)
            {
                addToList();
            }
        }
        public void SetMusicValue(float value)
        {
            _musicValue = value;
            _settingsSaveData.MusicWriteData(value);
            _audio.volume = Mathf.Lerp(0f, 0.2f, value);
        }
        private void addToList ()
        {
            for (int i = 0; i < _gameMusic.Length; i++)
            {
                _playMusic.Add(_gameMusic[i]);
            }
        }
        private AudioClip GetRandomClip(int arrayLenght)
        {
            return _playMusic[Random.Range(0, arrayLenght)];
        }
    }
}
