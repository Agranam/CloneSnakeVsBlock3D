using System;
using UnityEngine;

public class LevelBlockGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _rowBlockGeneratorPrefab;
    [SerializeField] private LevelLootGenerator _levelLootGenerator;

    public void LevelGenerator(int numberOfRows)
    {
        for (int i = 0; i < numberOfRows; i++)
        {
            GameObject rowOfBlocks = Instantiate(_rowBlockGeneratorPrefab, transform.position, Quaternion.identity);
            int zPosition = 20 + (i * 20);
            int lootValue = _levelLootGenerator.StacksOfLoot[i].AllLootValue;
            rowOfBlocks.GetComponent<RowBlockGenerator>().RowGenerator(zPosition, lootValue);
        }
    }
    
}
