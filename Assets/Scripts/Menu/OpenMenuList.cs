using DG.Tweening;
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
        [SerializeField] private GameObject Menu;
        
        private Vector2 _originalImageSize;
        private Vector3 _originalKnittingNeedlePosition;

        private void Awake()
        {
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

        public void BackPosition()
        {
            Move(_topKnittingNeedle, _originalKnittingNeedlePosition, _duration);
            Move(_botKnittingNeedle, _originalKnittingNeedlePosition, _duration);

            Resize(_topKnittingNeedle, _originalImageSize, _duration);
            Resize(_botKnittingNeedle, _originalImageSize, _duration);
            
            Invoke("DisabledImage", _duration);
        }

        private void Move(Transform selectedTransform, Vector3 targetPosition, float duration)
        {
            selectedTransform.DOMove(targetPosition, duration, false);
        }

        private void Resize(RectTransform rectTransform, Vector2 newSize, float duration)
        {
            rectTransform.DOSizeDelta(newSize, duration, false);
        }
        
        private void SavePositions()
        {
            _originalImageSize = _topKnittingNeedle.sizeDelta;
            _originalKnittingNeedlePosition = _topKnittingNeedle.position;
        }

        private void DisabledImage()
        {
            _botKnittingNeedleImage.enabled = false;
        }
    }
}
