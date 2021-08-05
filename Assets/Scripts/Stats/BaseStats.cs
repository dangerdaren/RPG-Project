using System;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass charClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect = null;
        [SerializeField] bool shouldUseModifiers = false;
        Experience experience;

        public event Action onLevelUp;

        int currentLevel = 1;

        private void Awake()
        {
            experience = GetComponent<Experience>();
        }

        private void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }

        private void Start()
        {
            currentLevel = CalculateLevel(); //todo this isn't ideal, but let's leave for now.
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifiers(stat)) * (1 + GetPercentageModifiers(stat)/100);
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStatValue(stat, charClass, GetLevel());
        }

        private float GetAdditiveModifiers(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float total = 0;

            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }

            return total;
        }

        private float GetPercentageModifiers(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
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

            for (int level = 1; level <= penultimateLevel; level++)
            {
                print($"Your current level is: {level}");
                float XPToLevelUp = progression.GetStatValue(Stat.ExperienceToLevelUp, charClass, level);
                print($"To get to level {level+1}, you NEED {XPToLevelUp} XP. You CURRENTLY have {currentXP} XP.");
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
