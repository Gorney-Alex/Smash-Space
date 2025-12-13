using System;
using MVVM;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UI.Binders
{
    public sealed class MonoViewBinder : MonoBehaviour
    {
        private enum BindingMode
        {
            FromInstance = 0,
            FromResolve = 1,
            FromResolveId = 2
        }

        [SerializeField]
        private BindingMode viewBinding;
        
        [SerializeField]
        private Object view;
        
#if UNITY_EDITOR
        [SerializeField]
        private MonoScript viewType;
#endif
        
        [SerializeField]
        private string viewTypeName; // Полное имя типа, например "UI.Views.MyView"
        
        [SerializeField]
        private string viewId;

        [Space(8)]
        [SerializeField]
        private BindingMode viewModelBinding;
        
        [SerializeField]
        private Object viewModel;
        
#if UNITY_EDITOR
        [SerializeField]
        private MonoScript viewModelType;
#endif
        
        [SerializeField]
        private string viewModelTypeName; // Полное имя типа, например "ViewModels.MyViewModel"
        
        [SerializeField]
        private string viewModelId;

        [Inject]
        private DiContainer diContainer;

        private IBinder _binder;

#if UNITY_EDITOR
        // Автоматически заполняем typeName из MonoScript в редакторе
        private void OnValidate()
        {
            if (viewType != null)
            {
                var type = viewType.GetClass();
                if (type != null)
                {
                    viewTypeName = type.AssemblyQualifiedName;
                }
            }
            
            if (viewModelType != null)
            {
                var type = viewModelType.GetClass();
                if (type != null)
                {
                    viewModelTypeName = type.AssemblyQualifiedName;
                }
            }
        }
#endif

        private void Awake()
        {
            _binder = this.CreateBinder();
        }

        private void OnEnable()
        {
            _binder.Bind();
        }

        private void OnDisable()
        {
            _binder.Unbind();
        }

        private IBinder CreateBinder()
        {
            Type viewTypeResolved = Type.GetType(viewTypeName);
            Type viewModelTypeResolved = Type.GetType(viewModelTypeName);
            
            if (viewTypeResolved == null && viewBinding != BindingMode.FromInstance)
            {
                Debug.LogError($"MonoViewBinder: Cannot resolve view type from '{viewTypeName}'");
            }
            
            if (viewModelTypeResolved == null && viewModelBinding != BindingMode.FromInstance)
            {
                Debug.LogError($"MonoViewBinder: Cannot resolve viewModel type from '{viewModelTypeName}'");
            }

            object view = this.viewBinding switch
            {
                BindingMode.FromInstance => this.view,
                BindingMode.FromResolve => this.diContainer.Resolve(viewTypeResolved),
                BindingMode.FromResolveId => this.diContainer.ResolveId(viewTypeResolved, this.viewId),
                _ => throw new Exception($"Binding type of view {this.viewBinding} is not found!")
            };

            object model = this.viewModelBinding switch
            {
                BindingMode.FromInstance => this.viewModel,
                BindingMode.FromResolve => this.diContainer.Resolve(viewModelTypeResolved),
                BindingMode.FromResolveId => this.diContainer.ResolveId(viewModelTypeResolved, this.viewModelId),
                _ => throw new Exception($"Binding type of viewModel {this.viewModelBinding} is not found!")
            };

            return BinderFactory.CreateComposite(view, model);
        }
    }
}