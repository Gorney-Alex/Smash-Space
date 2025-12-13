using ObjectData;
using UnityEngine;
using Zenject;

namespace Logic.App
{
    public sealed class GameSettings : IInitializable
    {
        private readonly GameSettingsData _data;

        public GameSettings(GameSettingsData data)
        {
            _data = data;
        }

        public void Initialize()
        {
            Application.targetFrameRate = _data.MaxFPS;
            QualitySettings.vSyncCount = 0;
        }
    }
}