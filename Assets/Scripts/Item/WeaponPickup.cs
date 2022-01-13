using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == ("Player"))
            {
                PassWeapon(other.gameObject);
            }
        }

        private void PassWeapon(GameObject player)
        {
            Fighter fighter = player.GetComponent<Fighter>();
            fighter.EquipWeapon(weapon);
            Destroy(gameObject);
        }
    }
}