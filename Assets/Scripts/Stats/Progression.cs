using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG Project/Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] CharacterClassProgression[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> classLookupTable = null;

        public float GetStatValue(Stat charStat, CharacterClass charClass, int charLevel)
        {
            BuildLookup();

            float[] levels = classLookupTable[charClass][charStat];

            if (levels.Length < charLevel) return 0;
            Debug.Log($"{name} is returning {charLevel}");
            return levels[charLevel];

        }

        public int GetAllLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();

            float[] levels = classLookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (classLookupTable != null) return;

            classLookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (CharacterClassProgression charClassProgression in characterClasses)
            {
                Dictionary<Stat, float[]> statLookupTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in charClassProgression.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                classLookupTable[charClassProgression.characterClass] = statLookupTable;

            }
        }

        [System.Serializable]
        class CharacterClassProgression
        {
            public CharacterClass characterClass;
                public ProgressionStat[] stats;

        }

                [System.Serializable]
                class ProgressionStat
                {
                    public Stat stat;
                        public float[] levels;
                }
    }
}

