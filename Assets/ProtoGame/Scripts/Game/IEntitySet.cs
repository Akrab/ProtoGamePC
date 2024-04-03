using Leopotam.EcsLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoGame.Assets.ProtoGame.Scripts.Game
{
    public interface IEntitySet
    {
        void Setup();
        void SetWorld(EcsWorld ecsWorld);
    }
}
