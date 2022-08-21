using UnityEngine;

namespace Menu
{
    public class SoundsEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _jumpInBasket;
        [SerializeField] private AudioSource _openMenu;


        public void Jump()
        {
            _jumpInBasket.Play();
        }

        public void OpenMenu()
        {
            _openMenu.Play();
        }
        
    }
}