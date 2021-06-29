using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private void Update()
        {
            SaveGame();
            LoadGame();
        }

        private void SaveGame()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<SavingSystem>().Save(defaultSaveFile);
            }
        }

        private void LoadGame()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                GetComponent<SavingSystem>().Load(defaultSaveFile);
            }
        }
    }
}
