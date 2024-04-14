using UnityEngine;

namespace ProtoGame.Game.Actor.Enemy
{
    public class ScannerObj : MonoBehaviour
    {
        [SerializeField] private Transform _rayPoint;
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(CONSTANTS.PLAYER_TAG))
            {
                Debug.LogError("I see");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(CONSTANTS.PLAYER_TAG))
            {

            }
        }
    }
}
