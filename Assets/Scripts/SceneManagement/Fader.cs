using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasFader;
        Coroutine currentActiveFade = null;

        private void Awake()
        {
            canvasFader = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediately()
        {
            canvasFader.alpha = 1;
        }

        public Coroutine FadeOut(float fadeTime)
        {
            return Fade(1, fadeTime);
        }

        public Coroutine FadeIn(float fadeTime)
        {
            return Fade(0, fadeTime);
        }

        public Coroutine Fade(float target, float fadeTime)
        {
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }

            currentActiveFade = StartCoroutine(FadeRoutine(target, fadeTime));
            return currentActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float fadeTime)
        {
            while (!Mathf.Approximately(canvasFader.alpha, target))
            {
                canvasFader.alpha = Mathf.MoveTowards(canvasFader.alpha, target, Time.deltaTime / fadeTime);
                yield return null;
            }
        }

    }
}
