using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";

        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;

            Debug.Log("Editing");
        }

        public string GetUniqueIdentifier()
        {
            return "";
        }

        public object CaptureState()
        {
            print($"Capturing state for: {GetUniqueIdentifier()}");
            return null;
        }

        public void RestoreState(object state)
        {
            print($"Restoring state for: {GetUniqueIdentifier()}");
        }
    }
}
