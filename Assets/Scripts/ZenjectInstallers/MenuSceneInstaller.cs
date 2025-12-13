using Buttons;
using UI.EmptyClasses;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ZenjectInstallers
{
    public sealed class MenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _keyButton;
        [SerializeField] private Button _backButton;
        
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private KeysMenu _keysMenu;
        
        public override void InstallBindings()
        {
            Debug.Log("INSTALLED MAIN_MENU");
            
            InstallMenu();
            InstallButtons();
        }

        private void InstallMenu()
        {
            Debug.Log("INSTALLED MENU");
            Container.Bind<MainMenu>().FromInstance(_mainMenu).AsSingle().NonLazy();
            Container.Bind<KeysMenu>().FromInstance(_keysMenu).AsSingle().NonLazy();
        }

        private void InstallButtons()
        {
            Debug.Log("INSTALLED MENU_BUTTONS");
            Container.BindInterfacesAndSelfTo<StartGameButton>().AsSingle().WithArguments(_startButton).NonLazy();
            Container.BindInterfacesAndSelfTo<ExitButton>().AsSingle().WithArguments(_exitButton).NonLazy();
            Container.BindInterfacesAndSelfTo<KeyboardButton>().AsSingle().WithArguments(_keyButton).NonLazy();
            Container.BindInterfacesAndSelfTo<BackButton>().AsSingle().WithArguments(_backButton).NonLazy();
        }
    }
}