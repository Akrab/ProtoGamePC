using UnityEngine;

namespace ProtoGame.Game.Weapons
{

    public interface IWeapon
    {
        void Fire();
        void Reload();
    }

    public class BaseWeaponObj : MonoBehaviour, IWeapon
    {
        public void Fire()
        {

        }

        public void Reload()
        {

        }
    }
}
