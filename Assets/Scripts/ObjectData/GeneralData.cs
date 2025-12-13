namespace ObjectData
{
    [System.Serializable]
    public class MapBorderData
    {
        public float MinBorderX;
        public float MaxBorderX;
        public float MinBorderY;
        public float MaxBorderY;
        public float SmallPushNumber;
    }
    
    [System.Serializable]
    public class GameSettingsData
    {
        public int MaxFPS;
    }

    [System.Serializable]
    public class GameLauncherData
    {
        public string MainMenuScene;
        public string GameScene;
    }
}