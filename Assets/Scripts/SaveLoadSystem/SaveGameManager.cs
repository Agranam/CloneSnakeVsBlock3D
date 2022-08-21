using System.IO;
using UnityEngine;

namespace SaveLoadSystem
{
    public static class SaveGameManager
    {
        public static SaveData CurrentSaveData = new SaveData();

        private const string _saveDirectory = "/SaveData/";
        private const string _fileName = "SaveGame.sav";
        
        
        public static void SaveGame()
        {
            string directory = Application.persistentDataPath + _saveDirectory;

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string json = JsonUtility.ToJson(CurrentSaveData, true);
            File.WriteAllText(directory + _fileName, json);
        }

        public static void LoadGame()
        {
            string fullPath = Application.persistentDataPath + _saveDirectory + _fileName;
            SaveData tempData = new SaveData();

            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                tempData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                Debug.LogError("Save file does not exist!");
            }

            CurrentSaveData = tempData;
        }
    }
}