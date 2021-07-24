using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasFader;

        private void Start()
        {
            canvasFader = GetComponent<CanvasGroup>();
        }


        public IEnumerator FadeOut(float fadeTime)
        {
            while (canvasFader.alpha < 1) //
            {
                canvasFader.alpha += Time.deltaTime / fadeTime;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float fadeTime)
        {
            while (canvasFader.alpha > 0) //
            {
                canvasFader.alpha -= Time.deltaTime / fadeTime;
                yield return null;
            }
        }

        public void FadeOutImmediately()
        {
            canvasFader.alpha = 1;
        }
    }
}
