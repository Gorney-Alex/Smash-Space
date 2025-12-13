using UI.ViewModel;
using UI.ViewModels;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public sealed class ViewModelsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("INSTALLED EMPTY_POINTS");

            Container.BindInterfacesAndSelfTo<PointViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HealthViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PositionViewModel>().AsSingle().NonLazy();
        }
    }
}