using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int loadScene = -1;

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
            yield return SceneManager.LoadSceneAsync(loadScene);
            print("Scene loaded.");
            Destroy(gameObject);
        }
    }
}
