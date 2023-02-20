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
    [SerializeField] private SoundsEffects _soundsEffects;
    
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
        _soundsEffects.PlaySoundEffect(5);
        _createdLevel = new GameObject("GameLevel" + CurrentLevel);
        UpdateLevel();
        _levelManager.GenerateLevel(_createdLevel.transform);
        _playerMovement.SetState(PlayerState.MovingInGame);
    }

    public void LevelComplete(int numberOfCells)
    {
        _soundsEffects.PlaySoundEffect(6);
        CurrentLevel++;
        UpdateLevel();
        _playerSaveData.TailLenghtWriteData(numberOfCells);
        _playerMovement.SetState(PlayerState.MovingInBackground);
        _endMenu.gameObject.SetActive(true);
        _endMenu.ShowEndMenu(false);
        DeleteLevel();
    }
    
    public void LevelLost()
    {
        _soundsEffects.PlaySoundEffect(4);
        _endMenu.gameObject.SetActive(true);
        _endMenu.ShowEndMenu(true);
    }

    public void Restart()
    {
        DeleteLevel();
        FinishLevel finishLevel = FindObjectOfType<FinishLevel>();
        if(finishLevel)
            Destroy(finishLevel.gameObject);
        StartGame();
    }

    public void ExitToMenu()
    {
        if (_createdLevel)
        {
            DeleteLevel();
        }
        FinishLevel finishLevel = FindObjectOfType<FinishLevel>();
        if(finishLevel)
            Destroy(finishLevel.gameObject);

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
        _gameUI.UpdateLevelText(CurrentLevel);
    }
}