using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoOutsideFromHome : QuestBase
{
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    public override IEnumerator WaitQuestTrigger()
    {
        yield return new WaitUntil(() => triggered);
    }
}
