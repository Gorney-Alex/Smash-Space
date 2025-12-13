using Logic.EmptyClasses;
using Logic.Interfaces;
using UnityEngine;
using Zenject;

namespace SpawnersLogic
{
    public class CommonSpawner<T> : ObjectPool<T> where T : IObjectPoolable
    {
        private Transform _spawnPosition;
        
        private int _maxAmount;
        
        [Inject]
        public void Construct(FirePoint spawnPosition)
        {
            _spawnPosition = spawnPosition.transform;
        }
    
        public void SetParametrs(ObjectsFactory factory, int maxAmount)
        {
            _maxAmount = maxAmount;
        
            Intialize(factory, _maxAmount);
        }

        public void GenerateObject()
        {
            if (TryGetObject(out T obj))
            {
                Vector2 spawnPos = _spawnPosition.position;
                obj.OwnerTransform.position = spawnPos;
                
                obj.OwnerTransform.rotation = _spawnPosition.rotation;
                
                obj.OwnerGameObject.SetActive(true);
            }
        }
    }
}