using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int currentCharLevel = 1;
        [SerializeField] CharacterClass charClass;
        [SerializeField] Progression progression = null;

        public float GetHealth()
        {
            return progression.GetHealth(charClass, currentCharLevel);
        }

        public float GetXpReward()
        {
            return 10;
        }

        
    }
}
