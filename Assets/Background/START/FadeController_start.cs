using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController_start : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    public IEnumerator FadeOut()
    {
        float t = 0f;
        Color c = fadeImage.color;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            c.a = Mathf.Clamp01(t);
            fadeImage.color = c;
            yield return null;
        }
    }
}
