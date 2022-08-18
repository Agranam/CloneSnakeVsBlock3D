using System.Collections.Generic;
using UnityEngine;

public class LevelLootGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _lootGeneratorPrefab;

    public List<LootGenerator> StacksOfLoot { get; private set; } = new List<LootGenerator>();
    
    public void LootGenerator(int numberOfRows)
    {
        for (int i = 0; i < numberOfRows; i++)
        {
            GameObject newGameObject = Instantiate(_lootGeneratorPrefab, transform.position, Quaternion.identity);
            StacksOfLoot.Add(newGameObject.GetComponent<LootGenerator>());
            StacksOfLoot[i].GetComponent<LootGenerator>().LootGeneratorBetweenRows(4 + (i * 20), 18 + (i * 20));
        }
    }
}
