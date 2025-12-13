using Logic.Lasers;
using Zenject;

namespace SpawnersLogic
{
    public class RedLaserSpawner : CommonSpawner<RedLaserView>
    {
        [Inject]
        public void Constructor(SpawnerRedLaserData data, ObjectsFactory factory)
        {
            SetParametrs(factory, data.MaxAmount);
        }
    }
}