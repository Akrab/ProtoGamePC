using ProtoGame.Infrastructure.Containers;
using ProtoGame.UI;
using UnityEngine.SceneManagement;
using Zenject;

namespace ProtoGame.Infrastructure.States
{
    public class GameGState : IGState
    {

        public void Enter(object data = null)
        {
            SceneManager.LoadSceneAsync(CONSTANTS.GAME_SCENE, LoadSceneMode.Additive);
        }

        public void Exit()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(CONSTANTS.GAME_SCENE));
        }
    }
}
