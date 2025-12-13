using Logic.Bullets;
using Logic.Lasers;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public sealed class AmmoInstaller : MonoInstaller
    {
        private LargeCaliberData _largeBulletCaliberData;
        private RedLaserData _redLaserData;
    
        public override void InstallBindings()
        {
            Debug.Log("INSTALLED AMMO");
            
            LoadJSON();
            InstallAmmoData();
            InstallBullets();
            InstallLasers();
        }

        private void LoadJSON()
        {
            _largeBulletCaliberData = JSONController.LoadJson<LargeCaliberData>("Data/LargeCaliberData");
            _redLaserData = JSONController.LoadJson<RedLaserData>("Data/RedLaserData");
        }

        private void InstallAmmoData()
        {
            Container.BindInterfacesAndSelfTo<LargeCaliberData>().FromInstance(_largeBulletCaliberData).AsSingle();
            Container.BindInterfacesAndSelfTo<RedLaserData>().FromInstance(_redLaserData).AsSingle();
        }

        private void InstallBullets()
        {
            Container.Bind<BulletsMovement>().AsTransient();
            Container.Bind<LargeCaliberModel>().AsTransient();
        }
        
        private void InstallLasers()
        {
            Container.Bind<LaserMovement>().AsTransient();
            Container.Bind<RedlaserModel>().AsTransient();
        }
    }
}