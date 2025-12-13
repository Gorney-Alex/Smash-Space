using MVVM;
using UI.Binders;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public sealed class BindersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("INSTALLED BINDERS");
            
            BinderFactory.RegisterBinder<TextBinder>();
        }
    }
}