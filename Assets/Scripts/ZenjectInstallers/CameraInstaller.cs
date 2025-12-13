using Logic.Camera;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public sealed class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CameraFollowerView _mainCameraTransform;
        
        public override void InstallBindings()
        {
            Debug.Log("INSTALLED CAMERA");
            
            InstallCamera();
        }

        private void InstallCamera()
        {
            Container.Bind<ICameraFollowerView>().FromInstance(_mainCameraTransform).AsSingle().NonLazy();
            Container.Bind<CameraFollowerView>().FromInstance(_mainCameraTransform).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraFollowerModel>().AsSingle().NonLazy();
        }
    }
}