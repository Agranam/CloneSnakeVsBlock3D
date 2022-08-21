using UnityEngine;

namespace SaveLoadSystem
{
    public class PlayerSaveData : MonoBehaviour
    {
        private PlayerData _myPlayerDataData = new PlayerData();

        public void TailLenghtWriteData(int snakeNumberOfCells)
        {
            _myPlayerDataData.SnakeNumberOfCells = snakeNumberOfCells;
            SaveSave();
        }
        public void TailLenghtReadData(out int snakeNumberOfCells)
        {
            LoadSave();
            snakeNumberOfCells = _myPlayerDataData.SnakeNumberOfCells;
        }
        public void LevelWriteData(int currentLevel)
        {
            _myPlayerDataData.GameLevel = currentLevel;
            SaveSave();
        }
        public void LevelReadData(out int currentLevel)
        {
            LoadSave();
            currentLevel = _myPlayerDataData.GameLevel;
        }
        private void LoadSave()
        {
            SaveGameManager.LoadGame();
            _myPlayerDataData = SaveGameManager.CurrentSaveData.PlayerData;
        }
        private void SaveSave()
        {
            SaveGameManager.CurrentSaveData.PlayerData = _myPlayerDataData;
            SaveGameManager.SaveGame();
        }
    }

    [System.Serializable]
    public struct PlayerData
    {
        public int SnakeNumberOfCells;
        public int GameLevel;
    }
}