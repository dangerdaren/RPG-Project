using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass charClass;
        [SerializeField] Progression progression = null;

        int currentLevel = 0;

        private void Start()
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                print("Leveled up!");
                // todo weird error here where player is getting more XP than he ought to.
            }
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStatValue(stat, charClass, GetLevel());
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }

            return currentLevel;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            if (experience == null) return startingLevel;

            float currentXP = experience.ExperiencePoints;
            int penultimateLevel = progression.GetAllLevels(Stat.ExperiencetoLevelUp, charClass);

            for (int level = 1; level < penultimateLevel; level++)
            {
                float levelUpXPAmount = progression.GetStatValue(Stat.ExperiencetoLevelUp, charClass, startingLevel);
                if (levelUpXPAmount > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }

    }
}
