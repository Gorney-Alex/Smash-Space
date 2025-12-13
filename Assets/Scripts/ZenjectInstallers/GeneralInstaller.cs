using Logic.EmptyClasses;
using Logic.GameSystems;
using Logic.Interfaces;
using Logic.Player;
using ObjectData;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public sealed class GeneralInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private ObjectPoolContainer _objectPoolContainer;

        private LimitsData _limitsData;
        private MapBorderData _mapBorderData;

        public override void InstallBindings()
        {
            Debug.Log("INSTALLED GENERAL");

            LoadJSON();
            InstallCommonData();
            InstallSignals();
            InstallObjects();
        }

        private void LoadJSON()
        {
            _limitsData = JSONController.LoadJson<LimitsData>("Data/LimitsData");
            _mapBorderData = JSONController.LoadJson<MapBorderData>("Data/MapBorderData");
        }

        private void InstallObjects()
        {
            Container.Bind<IPlayer>().FromInstance(_playerView).AsSingle();
            Container.Bind<ObjectPoolContainer>().FromInstance(_objectPoolContainer).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<MapBordersSystem>().AsSingle().NonLazy();
        }

        private void InstallCommonData()
        {
            Container.Bind<LimitsData>().FromInstance(_limitsData).AsSingle().NonLazy();
            Container.Bind<MapBorderData>().FromInstance(_mapBorderData).AsSingle().NonLazy();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<BigAsteroidDieSignal>();
            Container.DeclareSignal<BigAsteroidCompletelyDieSignal>();
            Container.DeclareSignal<SmallAsteroidDieSignal>();
            Container.DeclareSignal<AlienShipDieSignal>();
            Container.DeclareSignal<PlayerDieSignal>();
            Container.DeclareSignal<TogglePauseSignal>();
            Container.DeclareSignal<ToggleLockGameInputSignal>();
        }
    }
}