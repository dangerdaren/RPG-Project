using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool hasCinematicPlayed = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !hasCinematicPlayed)
            {
                GetComponent<PlayableDirector>().Play();
                hasCinematicPlayed = true;
            }
        }
    }
}


