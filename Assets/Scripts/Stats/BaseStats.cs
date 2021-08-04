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
            if (experience == null)
            {
                return startingLevel;
            }

            float currentXP = experience.ExperiencePoints;
            int penultimateLevel = progression.GetAllLevels(Stat.ExperienceToLevelUp, charClass);

            for (int level = startingLevel; level <= penultimateLevel; level++)
            {
                print($"Your current level is: {level}");
                float XPToLevelUp = progression.GetStatValue(Stat.ExperienceToLevelUp, charClass, level);
                print($"To get to level {level}, you NEED {XPToLevelUp} XP. You CURRENTLY have {currentXP} XP.");
                if (XPToLevelUp > currentXP)
                {
                    print($"Therefore, your level is now: {level}");
                    return level;
                }
            }

            return penultimateLevel + 1;
        }

    }
}
