using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class OpenMenuList : MonoBehaviour
    {
        [SerializeField] private float _duration = 2f;
        [SerializeField] private Vector2 _newImageSize = new Vector2(900f, 50f);
        [SerializeField] private SoundsEffects _soundsEffects;
        [SerializeField] private RectTransform _topKnittingNeedle;
        [SerializeField] private RectTransform _botKnittingNeedle;
        [SerializeField] private Transform _topPosition;
        [SerializeField] private Transform _botPosition;
        [SerializeField] private Image _botKnittingNeedleImage;
        [SerializeField] private GameObject _menu;
        [SerializeField] private Button[] _otherButtons;
        [SerializeField] private TextMeshProUGUI[] _menuButtonsTextMP;
        [SerializeField] private Image[] _menuImagesButtons;
        
        private TextMeshProUGUI[] _menuTextMP;
        private Image[] _menuImages;
        private Vector2 _originalImageSize;
        private Vector3 _originalKnittingNeedlePosition;

        private void Awake()
        {
            _menuTextMP = _menu.GetComponentsInChildren<TextMeshProUGUI>();
            _menuImages = _menu.GetComponentsInChildren<Image>();
            FastHideMenu();
            SavePositions();
            DisabledImage();
        }

        public void SetPosition()
        {
            SavePositions();
            PlaySound();
            _botKnittingNeedleImage.enabled = true;
            _topKnittingNeedle.DOMove(_topPosition.position, _duration);
            _botKnittingNeedle.DOMove(_botPosition.position, _duration);
            
            _topKnittingNeedle.DOSizeDelta(_newImageSize, _duration);
            _botKnittingNeedle.DOSizeDelta(_newImageSize, _duration);
        }

        public void ShowMenu()
        {
            foreach (var button in _otherButtons)
            {
                button.enabled = false;
            }
            _menu.SetActive(true);
            var seq= DOTween.Sequence();
            for (int i = 0; i < _menuButtonsTextMP.Length; i++)
            {
                seq.Insert(0,_menuButtonsTextMP[i].DOFade(0, 1));
            }
            for (int i = 0; i < _menuImagesButtons.Length; i++)
            {
                seq.Join(_menuImagesButtons[i].DOFade(0, 1));
            }

            seq.AppendInterval(0);
            for (int i = 0; i < _menuTextMP.Length; i++)
            {
                seq.Insert(1,_menuTextMP[i].DOFade(1, _duration + 1));
            }

            for (int i = 0; i < _menuImages.Length; i++)
            {
                seq.Join(_menuImages[i].DOFade(1, _duration + 1));
            }
        }

        private void FastHideMenu()
        {
            Color startTextColor = _menuTextMP[0].color;
            foreach (var textMeshProUGUI in _menuTextMP)
            {
                startTextColor.a = 0;
                textMeshProUGUI.color = startTextColor;
            }

            if (_menuImages.Length == 0) return;
                
            foreach (var image in _menuImages)
            {
                Color startImageColor = image.color;
                startImageColor.a = 0;
                image.color = startImageColor;
            }
        }
        
        public void SlowHideMenu()
        {
            foreach (var textMeshProUGUI in _menuTextMP)
            {
                textMeshProUGUI.DOFade(0, _duration);
            }

            foreach (var image in _menuImages)
            {
                image.DOFade(0, _duration);
            }
        }

        private void DisabledMenu()
        {
            _menu.SetActive(false);
        }
        
        public void BackPosition()
        {
            var seq = DOTween.Sequence();
            seq.AppendInterval(1);
            seq.Append(_topKnittingNeedle.DOMove(_originalKnittingNeedlePosition, _duration));
            seq.Join(_botKnittingNeedle.DOMove(_originalKnittingNeedlePosition, _duration));

            seq.Join(_topKnittingNeedle.DOSizeDelta(_originalImageSize, _duration));
            seq.Join(_botKnittingNeedle.DOSizeDelta(_originalImageSize, _duration));
            seq.AppendInterval(0);

            foreach (var menuText in _menuButtonsTextMP)
            {
                seq.Join(menuText.DOFade(1, 1));
            }
            foreach (var menuImage in _menuImagesButtons)
            {
                seq.Join(menuImage.DOFade(1, 1));
            }

            Invoke(nameof(PlaySound), 1);
            Invoke(nameof(DisabledImage), _duration + 1);
        }

        private void PlaySound()
        {
            _soundsEffects.PlaySoundEffect(1);
        }
        
        private void SavePositions()
        {
            _originalImageSize = _topKnittingNeedle.sizeDelta;
            _originalKnittingNeedlePosition = _topKnittingNeedle.position;
        }

        private void DisabledImage()
        {
            _botKnittingNeedleImage.enabled = false;
            _menu.SetActive(false);
            foreach (var button in _otherButtons)
            {
                button.enabled = true;
            }
        }
    }
}
