using UnityEngine;

namespace Logic.Interfaces
{
    public interface ICanBeCollision
    {
        void OnCustomCollision(GameObject other);
    }
}