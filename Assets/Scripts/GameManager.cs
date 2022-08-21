using System;
using Menu;
using SaveLoadSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CurrentLevel = 1;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerSaveData _playerSaveData;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private GameObject _endMenu;

    private void Awake()
    {
        _gameUI.gameObject.SetActive(false);
        //_playerMovement.gameObject.SetActive(false);
        _playerSaveData.LevelReadData(out CurrentLevel);

    }

    public void StartGame()
    {
        _gameUI.gameObject.SetActive(true);
        UpdateLevel();
        _levelManager.GenerateLevel();
        _playerMovement.SetState(PlayerState.MovingInGame);
    }

    public void LevelComplete()
    {
        CurrentLevel++;
        UpdateLevel();
        _playerMovement.SetState(PlayerState.MovingInBackground);
        //_playerMovement.enabled = false;
        _endMenu.SetActive(true);
    }

    private void UpdateLevel()
    {
        _playerSaveData.LevelWriteData(CurrentLevel);
        _gameUI.UpdateText(CurrentLevel);
    }
}