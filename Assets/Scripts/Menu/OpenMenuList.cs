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
            _botKnittingNeedleImage.enabled = true;
            Move(_topKnittingNeedle, _topPosition.position, _duration);
            Move(_botKnittingNeedle, _botPosition.position, _duration);

            Resize(_topKnittingNeedle, _newImageSize, _duration);
            Resize(_botKnittingNeedle, _newImageSize, _duration);
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
        
        public void HideMenu()
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
            
            // seq.AppendCallback(Move(_topKnittingNeedle, _originalKnittingNeedlePosition, _duration));
            // seq.AppendCallback(Move(_botKnittingNeedle, _originalKnittingNeedlePosition, _duration));
            //
            // seq.AppendCallback(Resize(_topKnittingNeedle, _originalImageSize, _duration));
            // seq.AppendCallback(Resize(_botKnittingNeedle, _originalImageSize, _duration));
            
            Invoke("DisabledImage", _duration + 1);
        }

        private TweenCallback Move(Transform selectedTransform, Vector3 targetPosition, float duration)
        {
            selectedTransform.DOMove(targetPosition, duration, false);
            return null;
        }

        private TweenCallback Resize(RectTransform rectTransform, Vector2 newSize, float duration)
        {
            rectTransform.DOSizeDelta(newSize, duration, false);
            return null;
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
