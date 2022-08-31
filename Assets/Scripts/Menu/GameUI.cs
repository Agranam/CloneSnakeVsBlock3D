using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private int _duration = 2;
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _nextLevelText;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Image _levelFill;

        [SerializeField] private TextMeshProUGUI[] _uITextMP;
        [SerializeField] private Image[] _uIImages;
        private float _startZ;
        private float _finishZ;
        
        private void Awake()
        {
            _uITextMP = GetComponentsInChildren<TextMeshProUGUI>();
            _uIImages = GetComponentsInChildren<Image>();
            _levelFill.fillAmount = 0f;
            FastHideUI();
        }

        public void SlowShowUI()
        {
            foreach (var textMeshProUGUI in _uITextMP)
            {
                textMeshProUGUI.DOFade(1, _duration);
            }
            foreach (var image in _uIImages)
            {
                image.DOFade(1, _duration);
            }
        }
        
        public void SlowHideUI()
        {
            foreach (var textMeshProUGUI in _uITextMP)
            {
                textMeshProUGUI.DOFade(0, _duration);
            }

            foreach (var image in _uIImages)
            {
                image.DOFade(0, _duration);
            }
        }

        private void FastHideUI()
        {
            Color startTextColor = _uITextMP[0].color;
            foreach (var textMeshProUGUI in _uITextMP)
            {
                startTextColor.a = 0;
                textMeshProUGUI.color = startTextColor;
            }

            if (_uIImages.Length == 0) return;
                
            foreach (var image in _uIImages)
            {
                Color startImageColor = image.color;
                startImageColor.a = 0;
                image.color = startImageColor;
            }
        }

        private void Update()
        {
            if(_playerMovement.CurrentPlayerState == PlayerState.MovingInGame)
                _levelFill.fillAmount = Mathf.InverseLerp(_startZ, _finishZ, _playerMovement.transform.position.z);
        }

        public void StartLevel(int startZ, int finishZ)
        {
            _startZ = startZ;
            _finishZ = finishZ;
        }

        public void UpdateLevelText(int currentLevel)
        {
            _currentLevelText.text = currentLevel.ToString();
            _nextLevelText.text = (currentLevel + 1).ToString();
        }
    }
}