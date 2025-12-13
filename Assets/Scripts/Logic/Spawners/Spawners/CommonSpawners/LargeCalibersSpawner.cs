using Logic.Bullets;
using Zenject;

namespace SpawnersLogic
{
    public class LargeCalibersSpawner : CommonSpawner<LargeCaliberView>
    {
        [Inject]
        public void Constructor(SpawnerLargeCaliberData data, ObjectsFactory factory)
        {
            SetParametrs(factory, data.MaxAmount);
        }
    }
}