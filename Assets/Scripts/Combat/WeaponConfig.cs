using RPG.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG Project/Weapons/New Weapon", order = 0)]
    public class WeaponConfig: ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animOverride = null;
        [SerializeField] Weapon equippedPrefab = null;

        [SerializeField] float weaponDamage = 5f;
          public float WeaponDamage => weaponDamage;
       
        [SerializeField] float weaponPercentageBonus = 0f;
            public float WeaponPercentageBonus => weaponPercentageBonus;

        [SerializeField] float weaponRange = 2f;
            public float WeaponRange => weaponRange;

        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";
        

        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestoryOldWeapon(rightHand, leftHand);

            Weapon weapon = null;

            if (equippedPrefab != null)
            {
                Transform handTransform = GetHandTransform(rightHand, leftHand);
                weapon = Instantiate(equippedPrefab, handTransform);
                weapon.gameObject.name = weaponName;
            }

            if (animOverride != null)
            {
                animator.runtimeAnimatorController = animOverride;
            }

            return weapon;
        }

        private void DestoryOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName); //check right hand for weapon.
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName); // if null, check left hand.
            }
            if (oldWeapon == null) return; // if null, no weapon, no worries.

            oldWeapon.name = "DESTROYING"; // otherwise, change the name to mark it, then destroy it.
            Destroy(oldWeapon.gameObject);
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

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHandTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

    }
}