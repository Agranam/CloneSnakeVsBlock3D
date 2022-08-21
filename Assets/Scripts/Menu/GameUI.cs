using TMPro;
using UnityEngine;

namespace Menu
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentLevelText;

        public void UpdateText(int currentLevel)
        {
            _currentLevelText.text = currentLevel.ToString();
        }
    }
}