using Leopotam.EcsLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoGame.Extensions
{
    public static class EcsExt
    {

        public static bool IsEmpty(this EcsFilter filter)
        {
            return filter.GetEntitiesCount() == 0;
        }
    }
}
