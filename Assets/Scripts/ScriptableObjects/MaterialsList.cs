using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerMaterials", menuName = "Configs/PlayersMaterials", order = 0)]
    public class MaterialsList : ScriptableObject
    {
        public Color[] playerColors;
        public Material[] floorMaterials;
        public Material[] wallMaterials;
        public Sprite[] backgroundImages;
    }
}
