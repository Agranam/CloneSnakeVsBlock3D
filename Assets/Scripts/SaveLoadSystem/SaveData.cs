namespace SaveLoadSystem
{   
    [System.Serializable]
    public class SaveData
    {
        public PlayerData PlayerData = new PlayerData();
        public SettingsData SettingsData = new SettingsData();
        public SkinsData SkinsData = new SkinsData();
    }
}
