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
        [SerializeField] private TextMeshProUGUI[] _menuTextMP;
        [SerializeField] private Image[] _menuImages;
        
        private Vector2 _originalImageSize;
        private Vector3 _originalKnittingNeedlePosition;

        private void Awake()
        {
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
            _menu.SetActive(true);
            var seq= DOTween.Sequence();
            seq.AppendInterval(1);
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
            Color startImageColor = _menuImages[0].color;
            foreach (var image in _menuImages)
            {
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
            
            Invoke(nameof(PlaySound), 1);
            Invoke(nameof(DisabledImage), _duration + 1);
        }

        private void PlaySound()
        {
            _soundsEffects.OpenMenu();
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
        }
    }
}
