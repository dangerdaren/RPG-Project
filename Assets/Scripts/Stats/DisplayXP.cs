using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RPG.Stats
{
    public class DisplayXP : MonoBehaviour
    {
        Experience xp;
        Text xpText;

        private void Awake()
        {
            xp = GameObject.FindWithTag("Player").GetComponent<Experience>();
            xpText = GetComponent<Text>();
        }

        private void Update()
        {
            ShowXP();
        }

        private void ShowXP()
        {
            xpText.text = String.Format("{0:0}", xp.ExperiencePoints);
        }
    }
}
