using UnityEngine;

public class Spot3Target : MonoBehaviour
{
    public GameObject objectToActivate;
    public AudioClip cleanSound;

    private void Awake()
    {
        // AudioSource가 없으면 추가
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }
    }

    public void Clean()
    {
        Debug.Log("✨ 정리됨!");

        // 1. 오브젝트 켜기
        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        // 2. 소리 재생용 임시 오브젝트 생성
        GameObject soundPlayer = new GameObject("OneShotSound");
        AudioSource audioSource = soundPlayer.AddComponent<AudioSource>();
        audioSource.clip = cleanSound;
        audioSource.volume = 0.5f;
        audioSource.Play();

        // 3. 재생 후 자동 제거
        Destroy(soundPlayer, cleanSound.length);

        // 4. 자기 자신 제거 (즉시)
        Destroy(gameObject);
    }
}
