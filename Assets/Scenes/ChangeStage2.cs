using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeStage2 : MonoBehaviour
{
    public string nextSceneName = "Stage2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ Player 감지, Stage2로 이동합니다!");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
