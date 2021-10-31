using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health state;
        private void Start()
        {
            state = GetComponent<Health>();
        }
        private void Update()
        {
            if (InteractWithCombat() || state.IsDead()) return;
            InteractWithMovement();
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) { continue; };

                //GameObject targetGameObject = target.gameObject;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
               GetComponent<Mover>().StartMoveAction(hit.point, 1f);
            }
            else
                print("Nothing to do.");
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
