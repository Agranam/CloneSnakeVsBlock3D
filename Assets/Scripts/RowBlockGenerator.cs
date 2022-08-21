using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RowBlockGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;
    
    private List<Block> _rowOfBlocks = new List<Block>();
    private List<Collider> _rowOfColliders = new List<Collider>();
    private List<int> _blockValue = new List<int>();
    
    public void RowGenerator(int zPosition, int lootValue)
    {
        Vector3 currentPosition = new Vector3(-9f, 0f, zPosition);
        CountBlockGenerator(lootValue);
        
        for (int i = 0; i < 6; i++)
        {
            SpawnBlock(i, currentPosition);
        }
    }

    private void CountBlockGenerator(int lootValue)
    {
        for (int i = 0; i < 6; i++)
        {
            _blockValue.Add(Random.Range(1, 5));
        }

        ChangeDifficulty(lootValue, 0);
    }

    private void ChangeDifficulty(int lootValue, int difficulty) // 0 - Easy, 1 - Normal, 2 - Hard.
    {
        switch (NumberOfMinBlocks(lootValue))
        {
            case < 2:
                SetValue(1,1, lootValue, 3 - difficulty);
                break;
            case > 4:
                SetValue(0,lootValue, 51, 1 + difficulty);
                break;
        }
    }

    private void SetValue(int minOrMax, int minBlock, int maxBlock, int numberOfCycles)
    {
        for (int i = 0; i < numberOfCycles; i++)
        {
            int minValue = _blockValue.Min();
            int maxValue = _blockValue.Max();
            int index = _blockValue.IndexOf(minOrMax == 0 ? minValue : maxValue);
            _blockValue[index] = Random.Range(minBlock, maxBlock);
        }
    }

    private int NumberOfMinBlocks(int lootValue)
    {
        int amountMinBlock = 0;
        for (int i = 0; i < _blockValue.Count; i++)
        {
            if (_blockValue[i] < lootValue)
            {
                amountMinBlock++;
            }
        }
        return amountMinBlock;
    }
    
    private void SpawnBlock(int index, Vector3 newPos)
    {
        GameObject block = Instantiate(_blockPrefab, newPos, Quaternion.identity, transform);
        block.transform.position = newPos + (Vector3.right * index * 3);
        _rowOfBlocks.Add(block.GetComponent<Block>());
        _rowOfColliders.Add(block.GetComponent<Collider>());
        _rowOfBlocks[index].SetBlockCount(_blockValue[index]);

    }
    public void DisabledBlock()
    {
        for (int i = 0; i < _rowOfBlocks.Count; i++)
        {
            _rowOfBlocks[i].enabled = _rowOfBlocks[i]._isActive == true ? true : false;
            _rowOfColliders[i].enabled = _rowOfBlocks[i]._isActive == true ? true : false;
        }
    }
}
