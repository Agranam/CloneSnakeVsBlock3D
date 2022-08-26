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
    [SerializeField] private EndMenu _endMenu;

    private GameObject _createdLevel;

    private void Awake()
    {
        _gameUI.gameObject.SetActive(false);
        _playerSaveData.LevelReadData(out CurrentLevel);
    }

    public void StartGame()
    {
        if (_createdLevel)
        {
            DeleteLevel();
        }
        _createdLevel = new GameObject("GameLevel" + CurrentLevel);
        _gameUI.gameObject.SetActive(true);
        UpdateLevel();
        _levelManager.GenerateLevel(_createdLevel.transform);
        _playerMovement.SetState(PlayerState.MovingInGame);
    }

    public void LevelComplete()
    {
        CurrentLevel++;
        UpdateLevel();
        _playerMovement.SetState(PlayerState.MovingInBackground);
        _endMenu.gameObject.SetActive(true);
        _endMenu.ShowMenu(false);
        DeleteLevel();
    }
    
    public void LevelLost()
    {
        _endMenu.gameObject.SetActive(true);
        _endMenu.ShowMenu(true);
    }

    public void Restart()
    {
        DeleteLevel();
        StartGame();
    }

    public void ExitToMenu()
    {
        if (_createdLevel)
        {
            DeleteLevel();
        }
        Debug.Log("Exit to Menu");
        _playerMovement.SetState(PlayerState.MovingInBackground);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }
    
    private void DeleteLevel()
    {
        _levelManager.DeleteLevel();
        Destroy(_createdLevel);
    }
    
    private void UpdateLevel()
    {
        _playerSaveData.LevelWriteData(CurrentLevel);
        _gameUI.UpdateText(CurrentLevel);
    }
}