using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlockGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _rowBlockGeneratorPrefab;
    [SerializeField] private GameObject _finish;
    [SerializeField] private LevelLootGenerator _levelLootGenerator;

    [SerializeField] private List<RowBlockGenerator> _rowBlocks = new List<RowBlockGenerator>();
    
    public void GenerateBlocks(Transform currentLevel, int numberOfRows, int generationPositionZ, int rowSpacing)
    {
        Vector3 currentPosition = new Vector3(0, 0, generationPositionZ);
        int offset = generationPositionZ + rowSpacing;
            
        for (int i = 0; i < numberOfRows; i++)
        {
            GameObject rowOfBlock = Instantiate(_rowBlockGeneratorPrefab, currentPosition, Quaternion.identity, currentLevel);
            _rowBlocks.Add(rowOfBlock.GetComponent<RowBlockGenerator>());
            int zPosition = offset + (i * rowSpacing);
            int lootValue = _levelLootGenerator.StacksOfLoot[i].AllLootValue;
            rowOfBlock.GetComponent<RowBlockGenerator>().RowGenerator(zPosition, lootValue);
        }

        Vector3 finishPosition = new Vector3(0f, 0f, offset + (numberOfRows * rowSpacing));
        Instantiate(_finish, finishPosition, Quaternion.identity);
    }
    public void DeleteBlocks()
    {
        _rowBlocks.Clear();
    }
}
