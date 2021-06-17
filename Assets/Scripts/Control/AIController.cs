using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float timeToWatchForPlayer = 5f;

        private GameObject player;
        private Fighter fighter;
        private Mover mover;
        private Health health;

        private Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            guardPosition = transform.position;
        }

        void Update()
        {
            if (health.IsDead) return;
            DetermineBehavior();
        }

        private void DetermineBehavior()
        {
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player)) // Attack State
            {
                timeSinceLastSawPlayer = 0f;
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer <= timeToWatchForPlayer) // Suspicion State
            {
                SuspicionBehavior();
            }
            else // Return to Guarding State
            {
                GuardBehavior();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        private void AttackBehavior()
        {
            print($"{this.name} is chasing {player.name}!");

            AttackPlayer();
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void GuardBehavior()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void AttackPlayer()
        {
            fighter.Attack(player);
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}


