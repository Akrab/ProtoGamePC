using ProtoGame.Game.Infrastructure;
using UnityEngine;

namespace ProtoGame.Game
{
    public class SmoothCameraFollow: BaseMonoBehaviour
    {
        [SerializeField] private float _smooth;
        [SerializeField] private float _Y_Border = -100f;

        public float Smooth => _smooth;
        public float Y_Border => _Y_Border;

    }
}
