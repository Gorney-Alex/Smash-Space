using Buttons;
using UI.EmptyClasses;
using UI.UIControllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ZenjectInstallers
{
    public class GameMenuInstaller : MonoInstaller
    {
        [SerializeField] private DeathMenu _deathMenu;
        [SerializeField] private StopMenu _stopMenu;
        [SerializeField] private Button _startMainMenuButton1;
        [SerializeField] private Button _startMainMenuButton2;
        [SerializeField] private Button _restartGameButton;
        [SerializeField] private Button _continueGameButton;
        
        public override void InstallBindings()
        {
            Debug.Log("INSTALLED GAME_MENU");
            
            InstallButtons();
            InstallUIViews();
            InstallUIControllers();
        }

        private void InstallButtons()
        {
            Container.BindInterfacesAndSelfTo<StartMainMenuButton>().AsCached().WithArguments(_startMainMenuButton1).NonLazy();
            Container.BindInterfacesAndSelfTo<StartMainMenuButton>().AsCached().WithArguments(_startMainMenuButton2).NonLazy();
            Container.BindInterfacesAndSelfTo<RestartGameButton>().AsSingle().WithArguments(_restartGameButton).NonLazy();
            Container.BindInterfacesAndSelfTo<ContinueButton>().AsSingle().WithArguments(_continueGameButton).NonLazy();
        }

        private void InstallUIControllers()
        {
            Container.BindInterfacesAndSelfTo<DeathMenuController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StopMenuController>().AsSingle().NonLazy();
        }
        
        private void InstallUIViews()
        {
            Container.Bind<DeathMenu>().FromInstance(_deathMenu).AsSingle().NonLazy();
            Container.Bind<StopMenu>().FromInstance(_stopMenu).AsSingle().NonLazy();
        }
    }
}