using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using RPG.Resources;
using GameDevTV.Utils;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float timeToWatchForPlayer = 5f;
        [SerializeField] private float waypointDwellTime = 0f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;

        [Range(0, 1)]
        [SerializeField] private float patrolSpeedFraction = 0.35f;

        private GameObject player;
        private Fighter fighter;
        private Mover mover;
        private Health health;

        private LazyValue<Vector3> guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int currentWaypointIndex = 0;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            guardPosition = new LazyValue<Vector3>(GetGuardPosition);
        }

        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        void Start()
        {
            guardPosition.ForceInit();
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
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer <= timeToWatchForPlayer) // Suspicion State
            {
                SuspicionBehavior();
            }
            else // Return to Guarding State
            {
                PatrolBehavior();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        private void AttackBehavior()
        {
            timeSinceLastSawPlayer = 0f;

            print($"{this.name} is chasing {player.name}!");

            AttackPlayer();
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardPosition.value;
            if (patrolPath !=null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint >= waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint <= waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex).position;

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


