using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class DisplayLevel : MonoBehaviour
    {
        GameObject player;

        Text levelText;
        float charLevel = 1;

        void Awake()
        {
            player = GameObject.FindWithTag("Player");

            charLevel = player.GetComponent<BaseStats>().GetLevel();
            levelText = GetComponent<Text>();
        }


        void Update()
        {
            ShowLevel();
        }

        private void ShowLevel()
        {
            charLevel = player.GetComponent<BaseStats>().GetLevel();
            levelText.text = String.Format("{0:0}", charLevel);
        }
    }
}
