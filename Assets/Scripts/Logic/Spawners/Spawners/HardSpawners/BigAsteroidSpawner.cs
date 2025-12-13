using System.Threading;
using Cysharp.Threading.Tasks;
using Logic.Enemies;
using Zenject;

namespace SpawnersLogic
{
    public class BigAsteroidSpawner : SpaceObjectSpawner<BigAsteroidView>
    {
        private CancellationTokenSource _cancelGenerateCycle;
        
        [Inject]
        public void Constructor(SpawnerBigAsteroidData data, ObjectsFactory factory)
        {
            SetParametrs(factory, data.SecondsBetweenSpawner, data.StartAmount, data.MinSpawnRadius, data.MaxSpawnRadius, data.MaxAmount);
        }
        
        private void OnEnable()
        {
            _cancelGenerateCycle = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _cancelGenerateCycle?.Cancel();
            _cancelGenerateCycle?.Dispose();
        }

        private void Start()
        {
            GeneratingObjects(_cancelGenerateCycle.Token).Forget();
        }
    }
}
