using SaveLoadSystem;
using ScriptableObjects;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private MaterialsList _materialsList;
    [SerializeField] private Renderer _wallRenderer;
    
    private void Start()
    {
        _wallRenderer.material = _materialsList.wallMaterials[SkinsSaveData.CurrentPlaygroundMaterial];
    }
}
