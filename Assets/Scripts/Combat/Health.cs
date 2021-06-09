using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        private bool isDead = false;
        public bool IsDead { get { return isDead; } }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0); //simple solution to keep health from dipping below 0.
            Debug.Log($"{this.name}'s current health: {healthPoints}");

            if (healthPoints < 1)
            {
                Die();
            }
        }

        public void Die()
        {
            if (!isDead) GetComponent<Animator>().SetTrigger("die");
            isDead = true;
        }
    }
}
