using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;

        Health target = null;
        float damage = 0;

        // Start is called before the first frame update
        void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        // Update is called once per frame
        void Update()
        {
            ShootAt();
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
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
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead) return;

            target.TakeDamage(damage);

            if (hitEffect != null)
            {
                GameObject hitFX = Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            Destroy(gameObject);
        }

    }
}