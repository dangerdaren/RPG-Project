using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        Text healthText;


        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthText = GetComponent<Text>();
        }

        private void Update()
        {
            DisplayHealth();
        }

        private void DisplayHealth()
        {
            //String.Format to remove decimal places.
            healthText.text = String.Format("{0:0}%", health.GetHealthPercentage());
        }

    }
}