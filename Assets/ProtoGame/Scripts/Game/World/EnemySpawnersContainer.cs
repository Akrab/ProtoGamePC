using ProtoGame.Game.Infrastructure;
using System.Collections.Generic;


namespace ProtoGame.Game.World
{
    public class EnemySpawnersContainer : BaseMonoBehaviour
    {
        private EnemySpawnerPoint[] _points;
        
        public IReadOnlyCollection<EnemySpawnerPoint> Points=> _points;
        protected override void SetupMB()
        {
            _points = GetComponentsInChildren<EnemySpawnerPoint>();

            foreach (var item in _points)
                item.Setup();
        }
    }
}
