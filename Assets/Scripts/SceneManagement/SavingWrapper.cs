using System.Collections;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        SavingSystem savingSystem;
        Fader fader;
        [SerializeField] float fadeInTime = 1f;

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
            fader = FindObjectOfType<Fader>();

            StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene()
        {
            yield return savingSystem.LoadLastScene(defaultSaveFile);
            fader.FadeOutImmediately();
            yield return fader.FadeIn(fadeInTime);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(LoadLastScene());
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                DeleteSaveGame();
            }
        }

        public void Save()
        {
            //GetComponent<SavingSystem>().Save(defaultSaveFile); todo remove this if it works.
            savingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            //GetComponent<SavingSystem>().Load(defaultSaveFile); todo remove this if it works.
            savingSystem.Load(defaultSaveFile);
        }

        public void DeleteSaveGame()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }

    }

}
