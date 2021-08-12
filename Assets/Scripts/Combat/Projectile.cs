using RPG.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifetime = 20f;
        [SerializeField] float lifeAfterImpact = 1f;
        [SerializeField] GameObject[] projectileParts = null;
        [SerializeField] UnityEvent onProjectileLaunch;
        [SerializeField] UnityEvent onProjectileImpact;


        Health target = null;
        GameObject instigator = null;
        float damage = 0;

        void Start()
        {
            onProjectileLaunch.Invoke();
            transform.LookAt(GetAimLocation());
            Destroy(gameObject, maxLifetime);
        }

        void Update()
        {
            ShootAt();
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;
        }

        private void ShootAt()
        {
            if (!target) return;

            if (isHoming && !target.IsDead) transform.LookAt(GetAimLocation());

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private Vector3 GetAimLocation()
        {
            // Get the height of the target and halve it to find the center location.
            // Then return + add that height to the target, which is at the bottom.
            // Return the value back as a Vector3.
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (!targetCapsule) return target.transform.position;
            return target.transform.position + Vector3.up * (targetCapsule.height / 2);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target)return;
            if (target.IsDead) return;

            speed = 0f;

            onProjectileImpact.Invoke();
            target.TakeDamage(instigator, damage);

            if (hitEffect != null)
            {

                GameObject hitFX = Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            DestroyProjectile();
        }

        private void DestroyProjectile()
        {
            foreach (GameObject part in projectileParts)
            {
                Destroy(part);
            }
            Destroy(gameObject, lifeAfterImpact);
        }

    }
}
