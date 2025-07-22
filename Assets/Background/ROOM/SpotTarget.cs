using UnityEngine;

public class SpotTarget : MonoBehaviour
{
    public AudioClip cleanSound; // 🔊 효과음

    public void Clean()
    {
        Debug.Log("🧽 정리됨!");

        // 🔊 소리 재생용 임시 오브젝트 만들기
        if (cleanSound != null)
        {
            GameObject soundPlayer = new GameObject("OneShotSound_Spot");
            AudioSource audioSource = soundPlayer.AddComponent<AudioSource>();
            audioSource.clip = cleanSound;
            audioSource.volume = 0.5f;
            audioSource.Play();

            // 소리 재생 후 파괴
            Destroy(soundPlayer, cleanSound.length);
        }

        // Spot 오브젝트는 즉시 파괴
        Destroy(gameObject);
    }
}
