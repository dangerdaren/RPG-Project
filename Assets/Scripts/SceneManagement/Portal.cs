using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int loadScene = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier origin;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeTime = 1f;
        [SerializeField] float fadeInDelay = .5f;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject); // this only works when the gameobject is in the root of the scene heirarchy.

            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            yield return fader.FadeOut(fadeTime); // yield return makes sure the coroutine has finished before moving on to the rest of the code.

            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(loadScene);

            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            savingWrapper.Save();

            yield return new WaitForSeconds(fadeInDelay); // Cool, didn't know you could just do this whenever!
            yield return fader.FadeIn(fadeTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.transform.position);
            player.transform.rotation = otherPortal.spawnPoint.transform.rotation;
            }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;

                if (destination == portal.origin)
                {
                    return portal;
                }

            }
            return null;
        }
    }
}
