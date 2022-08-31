using Menu;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _offsetPosition = 100;
    [SerializeField] private int _rowSpacing = 20;
    [SerializeField] private int _minNumberOfRows, _maxNumberOfRows;
    [SerializeField] private LevelLootGenerator _levelLootGenerator;
    [SerializeField] private LevelBlockGenerator _levelBlockGenerator;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameUI _gameUI;
    
    private int _numberOfRows;
    
    public void GenerateLevel(Transform currentLevel)
    {
        _numberOfRows = Random.Range(_minNumberOfRows, _maxNumberOfRows);
        int generationPosition = (int)_playerTransform.position.z + _offsetPosition;
        _levelLootGenerator.GenerateLoot(currentLevel, _numberOfRows, generationPosition, _rowSpacing);
        _levelBlockGenerator.GenerateBlocks(currentLevel, _numberOfRows, generationPosition, _rowSpacing);
        _gameUI.StartLevel(generationPosition, generationPosition + (_rowSpacing * (_numberOfRows + 1)));
    }

    public void DeleteLevel()
    {
        _levelLootGenerator.DeleteLoot();
        _levelBlockGenerator.DeleteBlocks();
    }
}
