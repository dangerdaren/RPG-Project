using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70f;
        [SerializeField] UnityEvent<float> takeDamage;

        private LazyValue<float> healthPoints;

        private bool isDead = false;
        public bool IsDead => isDead;

        float fullHealth;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }



        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        void Start()
        {
            healthPoints.ForceInit();
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print($"{gameObject.name} took damage: {damage}");

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0); //simple solution to keep health from dipping below 0.
            Debug.Log($"{this.name}'s current health: {healthPoints.value}");

            if (healthPoints.value < 1)
            {
                AwardXP(instigator);
                Die();
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void AwardXP(GameObject instigator)
        {
            Experience charExperience = instigator.GetComponent<Experience>();

            if (charExperience == null) return;

            float awardXP = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
            charExperience.GainExperience(awardXP);
        }

        public float GetHealthPercentage()
        {
            float healthFrac = GetHealthFraction();
            return healthFrac * 100;
        }

        public float GetHealthFraction()
        {
            fullHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            return healthPoints.value / fullHealth;
        }

        public void Die()
        {
            if (!isDead) GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;


            if (healthPoints.value < 1)
            {
                Die();
            }
        }
    }
}
