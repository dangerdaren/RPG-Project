using RPG.Core;
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
        [SerializeField] Projectile projectile = null;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Transform handTransform = GetHandTransform(rightHand, leftHand);
                Instantiate(equippedPrefab, handTransform);
            }

            if (animOverride != null)
            {
                animator.runtimeAnimatorController = animOverride;
            }
        }

        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);

            projectileInstance.transform.LookAt(GetAimLocation(target)); // For not-homing option.
        }

        private Vector3 GetAimLocation(Health target)
        {
            // Get the height of the target and halve it to find the center location.
            // Then return add that height to the target, which is at the bottom.
            // Return the value back as a Vector3.
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (!targetCapsule) return target.transform.position;
            return target.transform.position + Vector3.up * (targetCapsule.height / 2);
        }

    }
}