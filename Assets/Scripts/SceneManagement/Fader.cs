using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        static CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOutIn()
        {
            yield return FadeOut(2f);
            print("Faded out");
            yield return FadeIn(1f);
            print("Faded in");
        }


        public  IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1) // alpha is not 1 
            {
                canvasGroup.alpha += Time.deltaTime / time;// moving alpha toward 1
                yield return null;
            }
        }
       public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0) // alpha is not 1 
            {
                canvasGroup.alpha -= Time.deltaTime / time;// moving alpha toward 1
                yield return null;
            }
        }
    }
}