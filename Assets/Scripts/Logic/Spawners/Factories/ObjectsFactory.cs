using Logic.Interfaces;
using UnityEngine;

namespace SpawnersLogic
{
    public abstract class ObjectsFactory
    {
        public abstract T CreateObject<T>(Transform transform) where T : IObjectPoolable;
    }
}
