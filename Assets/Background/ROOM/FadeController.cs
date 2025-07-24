using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;         // FadePanel의 Image
    public float fadeDuration = 1f; // 암전 속도
    private float currentValue = 1f;

    void Start()
    {
        Color color = fadeImage.color;
        fadeImage.color = new Color(color.r, color.g, color.b, 1f);
    }

    // 암전
    public IEnumerator FadeOut()
    {
        float t = 0f;
        Color color = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }

    // 밝아짐
    public IEnumerator FadeIn()
    {
        float t = 0f;
        Color color = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }

    public IEnumerator Fade(float rate = 1f)
    {
        float t = 0f;
        Color color = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(currentValue, rate, t / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        currentValue = rate;
    }
}
