using System.Collections;
using UnityEngine;

public abstract class QuestBase : MonoBehaviour
{
    public abstract IEnumerator WaitQuestTrigger();
}
