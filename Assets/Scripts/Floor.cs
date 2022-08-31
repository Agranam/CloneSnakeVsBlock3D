using Menu;
using SaveLoadSystem;
using ScriptableObjects;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private MaterialsList _materialsList;
    [SerializeField] private Renderer _floorRenderer;
    
    private void Start()
    {
        _floorRenderer.material = _materialsList.floorMaterials[SkinsSaveData.CurrentPlaygroundMaterial];
    }
}
