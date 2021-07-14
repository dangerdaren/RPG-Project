using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
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

        public object CaptureState()
        {
            return hasCinematicPlayed;
        }

        public void RestoreState(object state)
        {
            hasCinematicPlayed = (bool)state;
        }
    }
}


