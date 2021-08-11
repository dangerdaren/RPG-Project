using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;

        void Update()
        {
            float healthFrac = healthComponent.GetHealthFraction();
            foreground.localScale = new Vector3(healthFrac, 1, 1);
        }
    }
}
