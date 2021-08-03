using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass charClass;
        [SerializeField] Progression progression = null;
        Experience experience;

        int currentLevel = 0;

        private void Awake()
        {
            experience = GetComponent<Experience>();
        }

        private void Start()
        {
            currentLevel = CalculateLevel();

            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            print($"newLevel = {newLevel}");
            print($"currentLevel = {currentLevel}");
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
            if (experience == null) return startingLevel;

            float currentXP = experience.ExperiencePoints;
            int penultimateLevel = progression.GetAllLevels(Stat.ExperienceToLevelUp, charClass);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStatValue(Stat.ExperienceToLevelUp, charClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }

    }
}
