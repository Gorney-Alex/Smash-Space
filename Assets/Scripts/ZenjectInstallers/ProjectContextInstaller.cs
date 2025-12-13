using Logic.App;
using ObjectData;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    [CreateAssetMenu(fileName = "ProjectInstaller", menuName = "Installers/New ProjectInstaller")]
    public sealed class ProjectContextInstaller : ScriptableObjectInstaller
    {
        private GameSettingsData _gameSettingsData;
        private GameLauncherData _gameLauncherData;
        
        public override void InstallBindings()
        {
            Debug.Log("INSTALLED PROJECT_CONTEXT");
            
            LoadJSON();
            InstallData();
            InstallContext();
        }

        private void LoadJSON()
        {
            _gameSettingsData = JSONController.LoadJson<GameSettingsData>("Data/GameSettingsData");
            _gameLauncherData = JSONController.LoadJson<GameLauncherData>("Data/GameLauncherData");
        }

        private void InstallData()
        {
            Container.Bind<GameSettingsData>().FromInstance(_gameSettingsData).AsSingle().NonLazy();
            Container.Bind<GameLauncherData>().FromInstance(_gameLauncherData).AsSingle().NonLazy();
        }

        private void InstallContext()
        {
            Container.Bind<ApplicationFinisher>().AsSingle().NonLazy();
            Container.Bind<GameLauncher>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameSettings>().AsSingle().NonLazy();
        }
    }
}