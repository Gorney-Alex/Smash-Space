using SpawnersLogic;

namespace Logic.Guns
{
    public sealed class MachineGun : LargeCalibersSpawner
    {
        public void Fire() => GenerateObject();
    }
}
