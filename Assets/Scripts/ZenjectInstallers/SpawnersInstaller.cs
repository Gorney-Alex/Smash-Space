using Logic.EmptyClasses;
using Logic.Enemies;
using SpawnersLogic;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public sealed class SpawnersInstaller : MonoInstaller
    {
        SpawnerBigAsteroidData _spawnerBigAsteroidData;
        SpawnerSmallAsteroidData _spawnerSmallAsteroidData;
        SpawnerAlienShipData _spawnerAlienShipData;
        SpawnerLargeCaliberData _spawnerLargeCaliberData;
        SpawnerRedLaserData _spawnerRedLaserData;
        BigAsteroidDeathHandlerData _bigAsteroidDeathHandlerData;

        [SerializeField] private FirePoint _firePoint;
        [SerializeField] private SpawnerRadius _spawnerRadius;
        [SerializeField] private SmallAsteroidSpawner _smallAsteroidSpawnerPrefab;

        public override void InstallBindings()
        {
            Debug.Log("INSTALLED SPAWNERS");

            LoadJSON();
            InstallData();
            InstallSpawners();
        }

        private void LoadJSON()
        {
            _spawnerBigAsteroidData = JSONController.LoadJson<SpawnerBigAsteroidData>("Data/SpawnerBigAsteroidData");
            _spawnerSmallAsteroidData =
                JSONController.LoadJson<SpawnerSmallAsteroidData>("Data/SpawnerSmallAsteroidData");
            _spawnerAlienShipData = JSONController.LoadJson<SpawnerAlienShipData>("Data/SpawnerAlienShipData");
            _spawnerLargeCaliberData = JSONController.LoadJson<SpawnerLargeCaliberData>("Data/SpawnerLargeCaliberData");
            _spawnerRedLaserData = JSONController.LoadJson<SpawnerRedLaserData>("Data/SpawnerRedLaserData");
            _bigAsteroidDeathHandlerData =
                JSONController.LoadJson<BigAsteroidDeathHandlerData>("Data/BigAsteroidDeathHandlerData");
        }

        private void InstallData()
        {
            Container.Bind<SpawnerBigAsteroidData>().FromInstance(_spawnerBigAsteroidData).AsSingle().NonLazy();
            Container.Bind<SpawnerSmallAsteroidData>().FromInstance(_spawnerSmallAsteroidData).AsSingle().NonLazy();
            Container.Bind<SpawnerAlienShipData>().FromInstance(_spawnerAlienShipData).AsSingle().NonLazy();
            Container.Bind<SpawnerLargeCaliberData>().FromInstance(_spawnerLargeCaliberData).AsSingle().NonLazy();
            Container.Bind<SpawnerRedLaserData>().FromInstance(_spawnerRedLaserData).AsSingle().NonLazy();
            Container.Bind<BigAsteroidDeathHandlerData>().FromInstance(_bigAsteroidDeathHandlerData).AsSingle()
                .NonLazy();
        }

        private void InstallSpawners()
        {
            Container.Bind<FirePoint>().FromInstance(_firePoint).NonLazy();
            Container.Bind<SpawnerRadius>().FromInstance(_spawnerRadius).NonLazy();

            Container.Bind<ObjectsFactory>().To<GeneralObjectsFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<SmallAsteroidSpawner>()
                .FromComponentInNewPrefab(_smallAsteroidSpawnerPrefab).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BigAsteroidDeathHandler>().AsSingle().NonLazy();
        }
    }
}