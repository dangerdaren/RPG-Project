using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasFader;

        private void Awake()
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

        public void FadeOutInstantly()
        {
            canvasFader.alpha = 1;
        }

        public IEnumerator FadeIn(float fadeTime)
        {
            while (canvasFader.alpha > 0) //
            {
                canvasFader.alpha -= Time.deltaTime / fadeTime;
                yield return null;
            }
        }
    }
}
