using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public FadeController_start fadeController;

    private bool isTransitioning = false;

    // 버튼에서 이 메서드를 호출
    public void StartGame()
    {
        if (!isTransitioning)
        {
            StartCoroutine(GoToStageScene());
        }
    }

    private System.Collections.IEnumerator GoToStageScene()
    {
        isTransitioning = true;
        yield return fadeController.FadeOut();
        SceneManager.LoadScene("Stage");
    }
}
