using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float expereincePoints = 0;

        public void GainExperience(float experience)
        {
            expereincePoints += experience;
        }
    }
}

