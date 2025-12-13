using ObjectData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Logic.App
{
    public sealed class GameLauncher
    {
        private readonly GameLauncherData _data;
        public GameLauncher(GameLauncherData data)
        {
            _data = data;
        }
        public void StartGame()
        {
            SceneManager.LoadScene(_data.GameScene);
            Time.timeScale = 1;
        }

        public void StartMainMenu()
        {
            SceneManager.LoadScene(_data.MainMenuScene);
            Time.timeScale = 1;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
    }
}