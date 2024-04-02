using UnityEditor;
using UnityEditor.SceneManagement;

namespace ProtoGame.Editor
{
    public class EditorCommands : UnityEditor.Editor
    {
        [MenuItem("ProtoGame/Play")]
        private static void Play()
        {
            EditorSceneManager.OpenScene("Assets/ProtoGame/Scenes/Lobby.unity");
            EditorApplication.isPlaying = true;
        }

        [MenuItem("ProtoGame/Load Lobby")]
        private static void LoadLobby()
        {
            EditorSceneManager.OpenScene("Assets/ProtoGame/Scenes/Lobby.unity");
            EditorApplication.isPlaying = false;
        }

        [MenuItem("ProtoGame/Load UI")]
        private static void LoadUI()
        {
            EditorSceneManager.OpenScene("Assets/ProtoGame/Scenes/UI.unity");
            EditorApplication.isPlaying = false;
        }

        [MenuItem("ProtoGame/Load Loading")]
        private static void LoadLoading()
        {
            EditorSceneManager.OpenScene("Assets/ProtoGame/Scenes/Loading.unity");
            EditorApplication.isPlaying = false;
        }

        [MenuItem("ProtoGame/Load Game")]
        private static void LoadGame()
        {
            EditorSceneManager.OpenScene("Assets/ProtoGame/Scenes/Game.unity");
            EditorApplication.isPlaying = false;
        }


    }
}
