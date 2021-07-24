using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using RPG.Resources;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter player;
        Health target;
        Text healthText;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            healthText = GetComponent<Text>();
        }

        private void Update()
        {
            target = player.GetTarget();
            DisplayHealth();
        }

        private void DisplayHealth()
        {
            if (target == null || target.IsDead)
            {
                healthText.text = "N/A";
            }
            else
            {
                //String.Format to remove decimal places.
                healthText.text = String.Format("{0:0}%", target.GetHealthPercentage());
            }

        }

    }
}
