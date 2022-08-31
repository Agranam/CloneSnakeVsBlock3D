using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Menu
{
    public class NameAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _strenght;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _randomness;
        [SerializeField] bool _fadeOut;
        [SerializeField] private TextMeshProUGUI _nameOfGameText;

        private Tween _shakeAnimation;
        private float _timer;
        
        public void Start()
        {
            ShakeAnimation();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            
            if(_timer > Random.Range(_duration + 1, _duration * 2))
            {
                _timer = 0;
                ShakeAnimation();
            }
        }
        
        private void ShakeAnimation()
        {
            _shakeAnimation = _nameOfGameText.transform.DOShakeScale(_duration, _strenght, _vibrato, _randomness, _fadeOut);
        }

        private void OnDisable()
        {
            _shakeAnimation.Kill();
        }
    }
}   