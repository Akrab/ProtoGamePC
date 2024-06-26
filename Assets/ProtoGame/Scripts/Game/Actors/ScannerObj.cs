using UnityEngine;

namespace ProtoGame.Game.Actor
{
    public class ScannerObj : MonoBehaviour
    {
        [SerializeField] private Transform _rayPoint;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                Debug.LogError("Maybe I See the player", gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.LogError("I Don't see the player", gameObject);
            }
        }
    }
}
