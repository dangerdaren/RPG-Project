using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Control;

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

        PlayerController playerController = null;


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
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;

            yield return fader.FadeOut(fadeTime); // yield return makes sure the coroutine has finished before moving on to the rest of the code.

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(loadScene);
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

            playerController.enabled = false;

            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            savingWrapper.Save();

            yield return new WaitForSeconds(fadeInDelay); // Cool, didn't know you could just do this whenever!
            fader.FadeIn(fadeTime);

            playerController.enabled = true;

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            //player.GetComponent<NavMeshAgent>().enabled = false;
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.transform.position);
            player.transform.rotation = otherPortal.spawnPoint.transform.rotation;
            //player.GetComponent<NavMeshAgent>().enabled = true;
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
