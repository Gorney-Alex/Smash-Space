using UnityEngine;

namespace Logic.Interfaces
{
    public interface ICanBeBordered
    {
        Transform OwnerTransform { get; }
    }
}