using SpawnersLogic;

namespace Logic.Guns
{
    public sealed class LaserGun : RedLaserSpawner
    {
        public void Fire() => GenerateObject();
    }
}
