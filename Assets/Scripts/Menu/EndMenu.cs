using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace Menu
{
    public class EndMenu : MonoBehaviour
    {
        [SerializeField] private int _duration = 2;
        [SerializeField] private GameObject Buttons;
        [SerializeField] private GameObject _restartButton;
        [SerializeField] private GameObject _continueButton;
        [SerializeField] private GameObject _menuButton;
        [SerializeField] private Image _menuPanel;

        private Image[] _images;
        private TextMeshProUGUI[] _textMeshProUGUI;
        
        private void Awake()
        {
            _images = Buttons.GetComponentsInChildren<Image>();
            _textMeshProUGUI = Buttons.GetComponentsInChildren<TextMeshProUGUI>();
            SetValue(0);
        }

        public void ShowEndMenu(bool isRestart)
        {
            _menuPanel.DOFillAmount(1, _duration);
            if (isRestart)
                Restart();
            else
                Continue();
        }

        public void HideEndMenu()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(0);
            foreach (var image in _images)
            {
                if (image)
                    sequence.Join(image.DOFade(0, 0.1f));
            }
            foreach (var textMeshProUGUI in _textMeshProUGUI)
            {
                if (textMeshProUGUI)
                    sequence.Join(textMeshProUGUI.DOFade(0, 0.1f));
            }
            sequence.Append(_menuPanel.DOFillAmount(0, _duration));

            Invoke(nameof(EndMenuDisabled), 1);
        }
        
        private void Restart()
        {
            SetValue(0);
            _restartButton.SetActive(true);
            _menuButton.SetActive(true);
            _continueButton.SetActive(false);
            ShowEndMenu(_duration);
        }

        private void Continue()
        {
            SetValue(0);
            _continueButton.SetActive(true);
            _menuButton.SetActive(true);
            _restartButton.SetActive(false);
            
            ShowEndMenu(_duration);
        }
        
        private void ShowEndMenu(int duration)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(duration);
            sequence.AppendInterval(duration);
            foreach (var image in _images)
            {
                if (image)
                    sequence.Join(image.DOFade(1, duration));
            }
            foreach (var textMeshProUGUI in _textMeshProUGUI)
            {
                if (textMeshProUGUI)
                    sequence.Join(textMeshProUGUI.DOFade(1, duration));
            }
        }
        
        private void SetValue(float value)
        {
            _menuPanel.fillAmount = value;
            Color startTextColor = _textMeshProUGUI[0].color;
            foreach (var textMeshProUGUI in _textMeshProUGUI)
            {
                startTextColor.a = value;
                textMeshProUGUI.color = startTextColor;
            }
            Color startImageColor = _images[0].color;
            foreach (var image in _images)
            {
                startImageColor.a = value;
                image.color = startImageColor;
            }
        }

        private void EndMenuDisabled()
        {
            gameObject.SetActive(false);
        }
    }
}