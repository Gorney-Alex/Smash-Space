using UnityEngine;

namespace Logic.Interfaces
{
    public interface IObjectPoolable
    {
        Transform OwnerTransform { get; }
        GameObject OwnerGameObject { get; }
    }
}