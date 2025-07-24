using UnityEngine;
using System.Collections;

public class Spot2Target : MonoBehaviour
{
    private int cleanCount = 0;
    private SpriteRenderer spriteRenderer;

    public AudioClip cleanSound;           // 🔊 효과음 연결용
    private AudioSource audioSource;       // 🔊 AudioSource 컴포넌트

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // AudioSource가 없으면 자동 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = 0.5f; // 🔉 볼륨 조절 (0.0 ~ 1.0)
        }
    }

    public void Clean()
    {
        cleanCount++;

        // 알파값 줄이기
        float alpha = Mathf.Clamp01(1f - (cleanCount / 3f));
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        Debug.Log($"🧼 얼룩별로 닦은 횟수: {cleanCount}/3");

        // 효과음 재생
        if (cleanSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(cleanSound);
        }

        // 마지막이면 지연 후 파괴
        if (cleanCount >= 3)
        {
            float delay = (cleanSound != null) ? cleanSound.length : 0f;
            StartCoroutine(DestroyAfterDelay(delay));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
