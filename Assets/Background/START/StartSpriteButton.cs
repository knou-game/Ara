using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartSpriteButton : MonoBehaviour
{
    public FadeController_start fadeController;     // 화면 암전용
    public AudioSource bgmAudioSource;              // 배경음악 오디오
    public AudioClip clickSound;                    // 클릭 효과음
    public float fadeOutDuration = 2f;              // BGM 페이드 길이
    public float waitBeforeLoad = 1f;               // 씬 전환까지 대기 시간

    private bool isClicked = false;

    void OnMouseDown()
    {
        if (isClicked) return;
        isClicked = true;

        // 🔊 클릭 효과음 재생
        if (clickSound != null)
        {
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        }

        StartCoroutine(HandleSceneTransition());
    }

    private IEnumerator HandleSceneTransition()
    {
        // 1. 화면 암전 시작
        if (fadeController != null)
            yield return fadeController.FadeOut();

        // 2. BGM 페이드아웃
        if (bgmAudioSource != null)
        {
            float startVolume = bgmAudioSource.volume;
            float t = 0f;

            while (t < fadeOutDuration)
            {
                t += Time.deltaTime;
                bgmAudioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutDuration);
                yield return null;
            }

            bgmAudioSource.Stop();
        }

        // 3. 기다렸다가 씬 전환
        yield return new WaitForSeconds(waitBeforeLoad);
        SceneManager.LoadScene("Stage");
    }
}
