using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

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
            health = Mathf.Max(health - damage, 0); //simple solution to keep health from dipping below 0.
            Debug.Log($"Object's current health: {health}");
        }
    }
}
