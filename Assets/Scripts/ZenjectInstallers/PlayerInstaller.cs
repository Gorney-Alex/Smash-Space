using Logic.Classes;
using Logic.Guns;
using Logic.InputSystems;
using Logic.Player;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private MachineGun _machineGun;
        [SerializeField] private LaserGun _laserGun;

        private PlayerData _playerData;

        public override void InstallBindings()
        {
            Debug.Log("INSTALLED PLAYER");

            LoadJSON();
            InstallData();
            InstallPlayer();
        }

        private void LoadJSON()
        {
            _playerData = JSONController.LoadJson<PlayerData>("Data/PlayerData");
        }

        private void InstallData()
        {
            Container.Bind<PlayerData>().FromInstance(_playerData).AsSingle().NonLazy();
        }

        private void InstallPlayer()
        {
            Container.Bind<MachineGun>().FromInstance(_machineGun).AsSingle();
            Container.Bind<LaserGun>().FromInstance(_laserGun).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<DesktopInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<MobileInputSystem>().AsSingle();
            Container.Bind<InputProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<DefineInput>().AsSingle();
            Container.Bind<LiveSystem>().AsSingle();
            Container.Bind<ArsenalSystem>().AsSingle().NonLazy();
            Container.Bind<PlayerModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WalletSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PositionSystem>().AsSingle();
            Container.Bind<EffectMovementSystem>().AsSingle().NonLazy();
        }
    }
}