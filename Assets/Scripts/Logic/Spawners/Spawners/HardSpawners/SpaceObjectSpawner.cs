using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Logic.EmptyClasses;
using Logic.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace SpawnersLogic
{
    public class SpaceObjectSpawner<T> : ObjectPool<T> where T : IObjectPoolable
    {
        private Transform _spawnerRadius;
    
        private float _secondsBetweenSpawner;
        private int _startAmount;
        private float _minSpawnRadius;
        private float _maxSpawnRadius;
        private int _maxAmount;

        [Inject]
        public void Construct(SpawnerRadius spawnerRadius)
        {
            _spawnerRadius = spawnerRadius.transform;
        }
    
        public void SetParametrs(ObjectsFactory factory, float secondsBetweenSpawner, int startAmount, float minSpawnRadius, float maxSpawnRadius, int maxAmount)
        {
            _secondsBetweenSpawner = secondsBetweenSpawner;
            _startAmount = startAmount;
            _minSpawnRadius = minSpawnRadius;
            _maxSpawnRadius = maxSpawnRadius;
            _maxAmount = maxAmount;
        
            Intialize(factory, _maxAmount);
        }
        
        public void SpawnAtPosition(Vector3 position)
        {
            if (TryGetObject(out T obj))
            {
                obj.OwnerTransform.position = position;
                obj.OwnerGameObject.SetActive(true);
            }
        }


        public async UniTask GeneratingObjects(CancellationToken token)
        {
            for (int i = 0; i < _startAmount; i++)
            {
                GenerateObject();
            }

            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_secondsBetweenSpawner), cancellationToken: token);


                var amountSpawnedObjects = _pool.Count(obj => obj.OwnerGameObject.activeSelf);

                if (amountSpawnedObjects < _startAmount)
                {
                    GenerateObject();
                }
            }
        }

        private void GenerateObject()
        {
            if (TryGetObject(out T obj))
            {
                float angle = Random.Range(0f, Mathf.PI * 2f);
                float distance = GetRandomDistance();

                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
                Vector2 spawnPos = (Vector2)_spawnerRadius.position + offset;
            
                obj.OwnerTransform.position = spawnPos;
                
                obj.OwnerGameObject.SetActive(true);
            }
        }

        private float GetRandomDistance() => Random.Range(_minSpawnRadius, _maxSpawnRadius);
    }
}