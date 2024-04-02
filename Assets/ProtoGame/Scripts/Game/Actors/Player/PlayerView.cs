using ProtoGame.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ProtoGame.Game.Actor.Player
{
    public class PlayerView : MonoBehaviour
    {
        private Rigidbody _rb;
        [Inject]
        public void Initialize()
        {
            _rb = GetComponent<Rigidbody>();
        }
    }
}
