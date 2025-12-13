using UnityEngine;

namespace Logic.Camera
{
    public interface ICameraFollowerView
    {
        Transform OwnerTransform { get; }
    }
}