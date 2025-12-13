 using Logic.Guns;

namespace Logic.Player
{
    public sealed class ArsenalSystem
    {
        private readonly MachineGun _machineGun;
        private readonly LaserGun _laserGun;
        
        public ArsenalSystem(MachineGun machineGun, LaserGun laserGun)
        {
            _machineGun = machineGun;
            _laserGun = laserGun;
        }

        public void GetFireMachineGun() => _machineGun.Fire();

        public void GetFireLaserGun() => _laserGun.Fire();
    }
}