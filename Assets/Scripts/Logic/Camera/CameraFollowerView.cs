using UnityEngine;

namespace Logic.Camera
{
    public sealed class CameraFollowerView : MonoBehaviour, ICameraFollowerView
    {
        public Transform OwnerTransform => transform;
    }
}