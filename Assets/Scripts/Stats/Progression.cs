using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG Project/Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] CharacterClassProgression[] characterClasses = null;

        public float GetHealth(CharacterClass charClass, int charLevel)
        {
            foreach (CharacterClassProgression charClassProg in characterClasses)
            {
                if (charClassProg.characterClass == charClass)
                {
                    return charClassProg.healthAtLevel[charLevel - 1];
                }
            }
            return 0;
        }

        [System.Serializable]
        class CharacterClassProgression
        {
            public CharacterClass characterClass;
            public float[] healthAtLevel;
            public int[] awardXP;
        }
    }
}

