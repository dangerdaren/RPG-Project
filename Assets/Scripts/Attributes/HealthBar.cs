using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas canvas = null;

        void Update()
        {
            UpdateHealthBar();

        }

        private void UpdateHealthBar()
        {
            float healthFrac = healthComponent.GetHealthFraction();
            canvas.enabled = IsVisibile(healthFrac);
            foreground.localScale = new Vector3(healthFrac, 1, 1);
        }

        private bool IsVisibile(float healthFrac)
        {
            if (healthFrac <= Mathf.Epsilon || Mathf.Approximately(healthFrac, 1f))
            {
                return false;
            }

            return true;
        }
    }
}
