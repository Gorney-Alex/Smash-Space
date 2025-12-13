using System.Collections.Generic;
using Logic.Interfaces;
using ObjectData;
using UnityEngine;
using Zenject;

namespace Logic.GameSystems
{
    public sealed class MapBordersSystem : ITickable
    {
        private List<ICanBeBordered> _sceneObjects;
        private readonly MapBorderData _data;

        public MapBordersSystem(MapBorderData data, IPlayer player)
        {
            _data = data;
            
            _sceneObjects = new List<ICanBeBordered>();
            
            if (player is ICanBeBordered)
            {
                _sceneObjects.Add(player as ICanBeBordered);
            }
        }

        public void Tick()
        {
            if (_sceneObjects.Count == 0) Debug.LogError("List is null");
            
            CheckSceneObjects();
        }
        
        public void AddObject(ICanBeBordered obj) => _sceneObjects.Add(obj);

        private void CheckSceneObjects()
        {
            foreach (var sceneObject in _sceneObjects)
            {
                if (sceneObject.OwnerTransform == null) continue;
                
                Vector2 currentPosition = sceneObject.OwnerTransform.position;

                if (currentPosition.x >= _data.MaxBorderX)
                {
                    currentPosition.x = _data.MinBorderX + _data.SmallPushNumber;
                }
                else if (currentPosition.x <= _data.MinBorderX)
                {
                    currentPosition.x = _data.MaxBorderX - _data.SmallPushNumber;
                }

                if (currentPosition.y >= _data.MaxBorderY)
                {
                    currentPosition.y = _data.MinBorderY + _data.SmallPushNumber;
                }
                else if (currentPosition.y <= _data.MinBorderY)
                {
                    currentPosition.y = _data.MaxBorderY - _data.SmallPushNumber;
                }
                
                sceneObject.OwnerTransform.position = currentPosition;
            }
        }
    }
}