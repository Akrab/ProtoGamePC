using Leopotam.EcsLite;
using ProtoGame.Extensions;
using UnityEngine;

namespace ProtoGame.Game.ECS
{
    public class EcsCameraFollowSys : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filterPlayerCamera;
        private EcsFilter _filterMovesPlayerCamera;
        private EcsFilter _filterPlayer;


        private EcsPool<EPlayer> _playerPool;
        private EcsPool<EPlayerCamera> _playerCameraPool;



        private void MovePlayer(ref EPlayerCamera playerCamera)
        {
            foreach (var entity in _filterPlayer)
            {
                ref var player = ref _playerPool.Get(entity);

                playerCamera.targetPostion = player.playerView.transform.position + playerCamera.offset;

                if (playerCamera.targetPostion.y <= playerCamera.Y_Border)
                    playerCamera.targetPostion.y = playerCamera.Y_Border;

                playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position, playerCamera.targetPostion, ref playerCamera.currentVelocity, playerCamera .smooth * Time.deltaTime);

            }

        }

        public void Init(IEcsSystems systems)
        {
            _filterPlayerCamera = systems.GetWorld().Filter<EPlayerCamera>().End();
            _filterPlayer = systems.GetWorld().Filter<EPlayer>().End();

            _filterMovesPlayerCamera = systems.GetWorld().Filter<EPlayerCameraMoves>().End();
            _playerCameraPool = systems.GetWorld().GetPool<EPlayerCamera>();
            _playerPool = systems.GetWorld().GetPool<EPlayer>();
        }

        public void Run(IEcsSystems systems)
        {
            if (_filterPlayerCamera.IsEmpty() || _filterMovesPlayerCamera.IsEmpty())
                return;


            foreach(var entity in _filterPlayerCamera) {

                ref var plCamera = ref _playerCameraPool.Get(entity);

                MovePlayer(ref plCamera);

            }


        }


    }
}
