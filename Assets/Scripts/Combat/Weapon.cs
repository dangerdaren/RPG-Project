using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon: ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponDamage = 5f;
            public float WeaponDamage => weaponDamage;
        [SerializeField] float weaponRange = 2f;
            public float WeaponRange => weaponRange;
        [SerializeField] bool isRightHanded = true;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Transform handTransform;
                if (isRightHanded) handTransform = rightHand;
                else handTransform = leftHand;
                Instantiate(equippedPrefab, handTransform);
            }

            if (animOverride != null)
            {
                animator.runtimeAnimatorController = animOverride;
            }
        }
    }
}