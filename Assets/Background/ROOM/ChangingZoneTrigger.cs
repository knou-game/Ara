using UnityEngine;

public class ChangingZoneTrigger : MonoBehaviour
{
    public Animator playerAnimator;                 // 플레이어의 Animator
    public RuntimeAnimatorController newOutfit;     // 갈아입을 AnimatorController
    public FadeController fadeController;           // Fade 스크립트 참조

    private bool playerInZone = false;

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
            StartCoroutine(ChangeOutfitRoutine());
        }
    }

    private System.Collections.IEnumerator ChangeOutfitRoutine()
    {
        yield return fadeController.FadeOut();               // 암전
        playerAnimator.runtimeAnimatorController = newOutfit; // 옷 교체
        yield return new WaitForSeconds(0.2f);               // 살짝 기다리고
        yield return fadeController.FadeIn();                // 밝아짐
    }
}
