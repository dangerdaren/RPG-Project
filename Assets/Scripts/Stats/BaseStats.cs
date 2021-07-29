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
        }

        private void Update()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                print("Leveled up!");
            }

        }

        public float GetStat(Stat stat)
        {
            return progression.GetStatValue(stat, charClass, GetLevel());
        }

        public int GetLevel()
        {
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
