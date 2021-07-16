using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float speed = 5f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ShootAt();
        }

        private void ShootAt()
        {
            if (!target) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        private Vector3 GetAimLocation()
        {
            // Get the height of the target and halve it to find the center location.
            // Then return add that height to the target, which is at the bottom.
            // Return the value back as a Vector3.
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (!targetCapsule) return target.position;

            return target.position + Vector3.up * (targetCapsule.height / 2);
        }
    }
}
