using UnityEngine;

namespace ProtoGame.Game.Infrastructure
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {

        protected bool _isSetup { get; private set; }

        private void Awake()
        {
            Setup();
        }

        protected virtual void SetupMB()
        {

        }

        public void Setup()
        {
            if (_isSetup) return;
            SetupMB();
            _isSetup = true;
        }
    }
}
