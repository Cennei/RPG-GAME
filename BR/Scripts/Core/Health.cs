using System;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(int damage)
        {
            healthPoints = Math.Max(healthPoints - damage, 0);
        }

        public void Death()
        {

            if (healthPoints == 0 && isDead == false)
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrenAction();
            }
        }
    }
}
