using UnityEngine;
using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.Resources;
using RPG.Stats;
using System.Collections.Generic;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;
        private Mover mover;
        private Weapon currentWeapon = null;


        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        void Start()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }

        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            else
            {
                mover.Cancel();
                AttackBehavior();
            }

        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
            Debug.Log($"Attacking the {combatTarget.name}!");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.transform.position, transform.position) <= currentWeapon.WeaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead; // return true if these things.
        }

        private void AttackBehavior()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {

                // This will trigger the Hit() event.
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation Event already built into imported animation
        private void Hit()
        {
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            //float damage = 5;

            if (target == null) return;

            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {

                target.TakeDamage(gameObject, damage);
            }
            print($"Damage delt: {damage}");
        }


        public void Cancel()
        {
            TriggerStopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void TriggerStopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.WeaponDamage;
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.WeaponPercentageBonus;
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
        } 


        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}
