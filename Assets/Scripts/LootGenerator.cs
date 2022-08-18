using System.Collections.Generic;
using UnityEngine;

public class LootGenerator : MonoBehaviour
{
    public int AllLootValue { get; private set; }
    [SerializeField] private GameObject _scarfPrefab;
    [SerializeField] private GameObject _sweaterPrefab;
    
    Dictionary<Vector2Int, Loot> _lootPosition = new Dictionary<Vector2Int, Loot>();
    private List<Loot> _lootStack = new List<Loot>();
    
    public void LootGeneratorBetweenRows(int startPos, int endPos)
    {
        int lootNumber = Random.Range(1, 5);
        int i = 0;
        while ((lootNumber - i) > 0)
        {
            int xPosition = Random.Range(-9, 5);
            int zPosition = Random.Range(startPos, endPos);
            if (CheckAllow(xPosition, zPosition))
            {
                Vector3 newPosition = new Vector3(xPosition, 0f, zPosition);
                SpawnLoot(i, newPosition);
                i++;
            }
        }
    }

    private void SpawnLoot(int index, Vector3 newPos)
    {
        int lootValue = Random.Range(1, 10);
        AllLootValue += lootValue;
        GameObject currentPrefab = lootValue < 6 ? _scarfPrefab : _sweaterPrefab;
        GameObject loot = Instantiate(currentPrefab, newPos, Quaternion.identity, transform);
        _lootStack.Add(loot.GetComponent<Loot>());
        _lootStack[index].SetNumberOfCells(lootValue);
        SavePosition((int)newPos.x, (int)newPos.z, _lootStack[index]);
    }
    
    private bool CheckAllow(int xPos, int zPos)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int z = 0; z < 4; z++)
            {
                Vector2Int coordinate = new Vector2Int(xPos + x, zPos + z);
                if (_lootPosition.ContainsKey(coordinate))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void SavePosition(int xPos, int zPos, Loot loot)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int z = 0; z < 4; z++)
            {
                Vector2Int coordinate = new Vector2Int(xPos + x, zPos + z);
                _lootPosition.Add(coordinate, loot);
            }
        }
    }
}
