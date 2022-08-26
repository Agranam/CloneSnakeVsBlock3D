using System.Collections.Generic;
using UnityEngine;

public class LevelLootGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _lootGeneratorPrefab;

    public List<LootGenerator> StacksOfLoot { get; private set; } = new List<LootGenerator>();
    
    public void GenerateLoot(Transform currentLevel, int numberOfRows, int generationPositionZ, int rowSpacing)
    {
        Vector3 currentPosition = new Vector3(0, 0, generationPositionZ);
        int offset = rowSpacing - 4;
        
        for (int i = 0; i < numberOfRows; i++)
        {
            GameObject stackOfLoot = Instantiate(_lootGeneratorPrefab, currentPosition, Quaternion.identity, currentLevel);
            StacksOfLoot.Add(stackOfLoot.GetComponent<LootGenerator>());
            int delta = generationPositionZ + (i * rowSpacing);
            StacksOfLoot[i].GetComponent<LootGenerator>().LootGeneratorBetweenRows(2 + delta, offset + delta);
        }
    }

    public void DeleteLoot()
    {
        StacksOfLoot.Clear();
    }
}
