using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float healthPoints = -1f;
        private bool isDead = false;
        public bool IsDead => isDead;

        float fullHealth;

        void Awake()
        {

            fullHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            if (healthPoints < 0 && !isDead)
            {
                healthPoints = fullHealth;
                Debug.Log("Blarg!");
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0); //simple solution to keep health from dipping below 0.
            Debug.Log($"{this.name}'s current health: {healthPoints}");

            if (healthPoints < 1)
            {
                AwardXP(instigator);
                Die();
            }
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
            float healthFrac = healthPoints / fullHealth;
            return healthFrac * 100;
        }

        public void Die()
        {
            if (!isDead) GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;


            if (healthPoints < 1)
            {
                Die();
            }
        }
    }
}
