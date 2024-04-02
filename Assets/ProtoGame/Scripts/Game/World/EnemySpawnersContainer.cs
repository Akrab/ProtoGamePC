using ProtoGame.Game.Infrastructure;


namespace ProtoGame.Game.World
{
    public class EnemySpawnersContainer : BaseMonoBehaviour
    {
        private EnemySpawnerPoint[] _points;
        protected override void SetupMB()
        {
            _points = GetComponentsInChildren<EnemySpawnerPoint>();

            foreach (var item in _points)
                item.Setup();
        }
    }
}
