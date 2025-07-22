using UnityEngine;

public class Spot4Target : MonoBehaviour
{
    public Vector3 closeOffset = new Vector3(-1f, 0, 0); // 닫힘 방향
    public float moveSpeed = 2f;
    public AudioClip cleanSound; // 효과음

    private Vector3 targetPos;
    private bool isClosing = false;

    private void Start()
    {
        targetPos = transform.position + closeOffset;
    }

    public void Clean()
    {
        Debug.Log("📦 서랍장 닫힘 시작!");
        isClosing = true;

        // 🔊 소리 재생용 임시 오브젝트 생성
        if (cleanSound != null)
        {
            GameObject soundPlayer = new GameObject("OneShotSound_Spot4");
            AudioSource audioSource = soundPlayer.AddComponent<AudioSource>();
            audioSource.clip = cleanSound;
            audioSource.volume = 0.5f;
            audioSource.Play();

            Destroy(soundPlayer, cleanSound.length);
        }
    }

    private void Update()
    {
        if (isClosing)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                Debug.Log("✅ 서랍장 닫힘 완료!");
                Destroy(gameObject);
            }
        }
    }
}
