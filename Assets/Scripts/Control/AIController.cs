using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float dwellingOnPointTime = 3f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.4f;

        Fighter fighter;
        Health state;
        Mover mover;
        GameObject player;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceOnPoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            state = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (state.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceOnPoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    timeSinceOnPoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (timeSinceOnPoint > dwellingOnPointTime)
            {
                GetComponent<Mover>().MoveTo(nextPosition, patrolSpeedFraction);
            }
        }

        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrenAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        // called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
