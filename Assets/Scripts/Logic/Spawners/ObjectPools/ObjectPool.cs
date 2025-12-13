using System.Collections.Generic;
using System.Linq;
using Logic.EmptyClasses;
using UnityEngine;
using Logic.Interfaces;
using Zenject;

namespace SpawnersLogic
{
    public class ObjectPool<T> : MonoBehaviour where T : IObjectPoolable
    {
        private Transform _container;
        
        protected List<T> _pool;

        [Inject]
        public void Construct(ObjectPoolContainer container)
        {
            _container = container.transform;
        }

        public void Intialize(ObjectsFactory factory, int maxAmount)
        {
            _pool = new List<T>(maxAmount);
        
            for (int i = 0; i < maxAmount; i++)
            {
                T instance = factory.CreateObject<T>(_container);
                
                if (instance == null)
                {
                    Debug.LogError($"Failed to create unit {i} in pool initialization");
                    continue;
                }
                
                _pool.Add(instance);
                instance.OwnerGameObject.SetActive(false);
            }
        }

        public bool TryGetObject(out T result)
        {
            result = _pool.FirstOrDefault(obj => obj.OwnerGameObject.activeSelf == false);
            return result != null;
        }
    }
}