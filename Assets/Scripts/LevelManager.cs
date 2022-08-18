using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _minNumberOfRows, _maxNumberOfRows;
    [SerializeField] private LevelLootGenerator _levelLootGenerator;
    [SerializeField] private LevelBlockGenerator _levelBlockGenerator;

    private int _numberOfRows;
    
    private void Awake()
    {
        _numberOfRows = Random.Range(_minNumberOfRows, _maxNumberOfRows);

        _levelLootGenerator.LootGenerator(_numberOfRows);
        _levelBlockGenerator.LevelGenerator(_numberOfRows);
    }
}
