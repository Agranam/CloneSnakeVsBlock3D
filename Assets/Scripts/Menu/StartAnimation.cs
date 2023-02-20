using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class StartAnimation : MonoBehaviour
    {
        [SerializeField] private int _duration = 2;
        [SerializeField] private Image _mineMenuImage;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private GameObject _mineMenu;
        
        private TextMeshProUGUI[] _menuButtonsTextMP;
        private Image[] _menuImagesButtons;
        private Button[] _menuButtons;

        private void Start()
        {
            _menuButtonsTextMP = GetComponentsInChildren<TextMeshProUGUI>();
            _menuImagesButtons = GetComponentsInChildren<Image>();
            _menuButtons = GetComponentsInChildren<Button>();
        }

        public void SlowHideMenu()
        {
            _mineMenuImage.DOFillAmount(0, _duration);
            foreach (var textMeshProUGUI in _menuButtonsTextMP)
            {
                textMeshProUGUI.DOFade(0, _duration);
            }
            foreach (var image in _menuImagesButtons)
            {
                image.DOFade(0, _duration);
            }
            foreach (var button in _menuButtons)
            {
                button.enabled = false;
            }
            Invoke(nameof(DisabledMineMenu), _duration);
        }

        public void SlowShowMenu()
        {
            _mineMenu.SetActive(true);
            _mineMenuImage.DOFillAmount(1, 2);
            foreach (var textMeshProUGUI in _menuButtonsTextMP)
            {
                textMeshProUGUI.DOFade(1, _duration);
            }
            foreach (var image in _menuImagesButtons)
            {
                image.DOFade(1, _duration);
            }
            foreach (var button in _menuButtons)
            {
                button.enabled = true;
            }
            _mineMenu.SetActive(true);
        }

        private void DisabledMineMenu()
        {
            _mineMenu.SetActive(false);
        }
    }
}