using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG Project/Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] CharacterClassProgression[] characterClasses = null;

        public float GetStat(Stat charStat, CharacterClass charClass, int charLevel)
        {
            foreach (CharacterClassProgression progressionClass in characterClasses)
            {
                if (progressionClass.characterClass != charClass) continue;

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    if (progressionStat.stat != charStat) continue;


                    if (progressionStat.levels.Length < charLevel) continue;

                    return progressionStat.levels[charLevel - 1];
                }

            }
            return 0;
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

