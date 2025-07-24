using UnityEngine;

public class ChangingZoneTrigger : MonoBehaviour
{
    public Animator playerAnimator;                         // 플레이어의 Animator
    public RuntimeAnimatorController originalOutfit;        // 원래 옷
    public RuntimeAnimatorController newOutfit;             // 새 옷
    public FadeController fadeController;                   // 암전 효과
    public AudioClip changeClothesSound;                    // 효과음

    private bool playerInZone = false;
    private bool isWearingNewOutfit = false;                // 현재 착용 상태

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.Space))
        {
            // ✅ 방 청소가 끝나지 않으면 옷 못 갈아입음
            if (!RoomCleanupManager.isRoomCleaned)
            {
                Debug.Log("🧼 방을 다 치운 후에 옷을 갈아입을 수 있어요!");
                return;
            }

            StartCoroutine(ChangeOutfitRoutine());
        }
    }

    private System.Collections.IEnumerator ChangeOutfitRoutine()
    {
        yield return fadeController.FadeOut(); // 암전

        // 🔊 효과음 재생
        if (changeClothesSound != null)
        {
            GameObject soundPlayer = new GameObject("OneShotSound_ChangingZone");
            AudioSource audioSource = soundPlayer.AddComponent<AudioSource>();
            audioSource.clip = changeClothesSound;
            audioSource.volume = 0.5f;
            audioSource.Play();
            Destroy(soundPlayer, changeClothesSound.length);
        }

        // 옷 토글
        if (isWearingNewOutfit)
        {
            playerAnimator.runtimeAnimatorController = originalOutfit;
        }
        else
        {
            playerAnimator.runtimeAnimatorController = newOutfit;
        }

        isWearingNewOutfit = !isWearingNewOutfit;

        yield return new WaitForSeconds(0.2f);
        yield return fadeController.FadeIn(); // 밝아짐
    }
}
