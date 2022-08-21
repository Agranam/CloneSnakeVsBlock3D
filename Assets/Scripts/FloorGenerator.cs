using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField] private int _startNumberOfPlatforms;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private Transform _player;
    [SerializeField] private int _platformLenght;

    private List<GameObject> _createdPlatforms = new List<GameObject>();
    private int _spawnPosition;

    private void Awake()
    {
        _spawnPosition -= 20;
        for (int i = 0; i < _startNumberOfPlatforms; i++)
        {
            CreatePlatform();
        }
    }

    private void Update()
    {
        if (!_player) return;
        
        if (_player.position.z > _spawnPosition - ((_startNumberOfPlatforms - 1) * _platformLenght))
        {
            CreatePlatform();
            DeletePlatform();
        }
    }

    private void CreatePlatform()
    {
        GameObject createdPlatform = Instantiate(_platformPrefab, 
            transform.forward * _spawnPosition, Quaternion.identity, transform);
        _createdPlatforms.Add(createdPlatform);
        _spawnPosition += _platformLenght;
    }

    private void DeletePlatform()
    {
        Destroy(_createdPlatforms[0]);
        _createdPlatforms.RemoveAt(0);
    }
}