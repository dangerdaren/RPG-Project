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

        private GameObject player;
        private Fighter fighter;
        private Mover mover;
        private Health health;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead) return;
            CheckForChase();
        }

        private void CheckForChase()
        {
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                ChasePlayer();
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        private void ChasePlayer()
        {
            print($"{this.name} is chasing {player.name}!");

            AttackPlayer();

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


