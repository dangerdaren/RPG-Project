using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class DisplayLevel : MonoBehaviour
    {
        GameObject player;

        Text levelText;
        //LazyValue<float> charLevel;

        void Awake()
        {
            player = GameObject.FindWithTag("Player");
            levelText = GetComponent<Text>();
        }

        void Update()
        {
            ShowLevel();
        }

        private void ShowLevel()
        {
            float charLevel = player.GetComponent<BaseStats>().GetLevel();
            levelText.text = String.Format("{0:0}", charLevel);
        }
    }
}
