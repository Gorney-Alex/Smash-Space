using System;
using Logic.Bullets;
using Logic.Enemies;
using Logic.GameSystems;
using Logic.Interfaces;
using Logic.Lasers;
using UnityEngine;
using Zenject;

namespace SpawnersLogic
{
    public sealed class GeneralObjectsFactory : ObjectsFactory
    {
        private static readonly Type[] SupportedUnitTypes = new Type[]
        {
            typeof(BigAsteroidView), 
            typeof(SmallAsteroidView), 
            typeof(AlienShipView),
            typeof(LargeCaliberView),
            typeof(RedLaserView)
        };
        
        private MapBordersSystem _mapBordersSystem;
        private DiContainer _container;
        
        [Inject]
        public void Construct(MapBordersSystem mapBordersSystem, DiContainer container)
        {
            _mapBordersSystem = mapBordersSystem;
            _container = container;
        }
        
        public override T CreateObject<T>(Transform parent)
        {
            var givenType = typeof(T);
            var prefab = Resources.Load<GameObject>($"Prefabs/{givenType.Name}");

            if (prefab == null) Debug.LogError($"Could not find prefab {givenType.Name}");
            
            var instance = _container.InstantiatePrefabForComponent<T>(prefab, parent);

            if (instance is ICanBeBordered border)
            {
                _mapBordersSystem.AddObject(border);
            }
            
            Debug.Log($"Created {givenType.Name}");

            return instance;
        }
    }
}