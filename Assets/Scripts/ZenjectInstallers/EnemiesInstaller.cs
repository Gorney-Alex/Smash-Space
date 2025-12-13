using Logic.Enemies;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public sealed class EnemiesInstaller : MonoInstaller
    {
        private BigAsteroidData _bigAsteroidData;
        private SmallAsteroidData _smallAsteroidData;
        private AlienShipData _alienShipData;

        public override void InstallBindings()
        {
            Debug.Log("INSTALLED ENEMIES");

            LoadJSON();
            InstallData();
            InstallEnemies();
        }

        private void LoadJSON()
        {
            _bigAsteroidData = JSONController.LoadJson<BigAsteroidData>("Data/BigAsteroidData");
            _alienShipData = JSONController.LoadJson<AlienShipData>("Data/AlienShipData");
            _smallAsteroidData = JSONController.LoadJson<SmallAsteroidData>("Data/SmallAsteroidData");
        }

        private void InstallData()
        {
            Container.Bind<BigAsteroidData>().FromInstance(_bigAsteroidData).AsSingle();
            Container.Bind<AlienShipData>().FromInstance(_alienShipData).AsSingle();
            Container.Bind<SmallAsteroidData>().FromInstance(_smallAsteroidData).AsSingle();
        }

        private void InstallEnemies()
        {
            Container.Bind<BigAsteroidModel>().AsTransient();
            Container.Bind<BigAsteroidMovement>().AsTransient();
            Container.Bind<BigAsteroidLive>().AsTransient();

            Container.Bind<SmallAsteroidModel>().AsTransient();
            Container.Bind<SmallAsteroidMovement>().AsTransient();
            Container.Bind<SmallAsteroidLive>().AsTransient();

            Container.Bind<AlienShipModel>().AsTransient();
            Container.Bind<AlienShipMovement>().AsTransient();
            Container.Bind<AlienShipLive>().AsTransient();
        }
    }
}