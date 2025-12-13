using Logic.Interfaces;
using UnityEngine;
using Zenject;

namespace Logic.Camera
{
    public sealed class CameraFollowerModel : ILateTickable
    {
        private readonly IPlayer _player;
        private readonly ICameraFollowerView _camera;

        public CameraFollowerModel(IPlayer playerPosition, ICameraFollowerView cameraPosition)
        {
            _player = playerPosition;
            _camera = cameraPosition;
        }

        public void LateTick()
        {
            _camera.OwnerTransform.position = new Vector3(
                _player.OwnerTransform.position.x,
                _player.OwnerTransform.position.y,
                _player.OwnerTransform.position.z
            );
        }
    }
}