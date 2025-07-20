using System.Data.Common;
using UnityEngine;

public class IntegrationController : MonoBehaviour
{
    [SerializeField]
    DialogueEvent dialogue;

    void Awake()
    {
        while (!LineDatabase.isFinish);
        dialogue.dialogues = LineDatabase.instance.GetDialogue(dialogue.scene);
    }

    void Start()
    {
        ScenarioManager.Instance.ShowDialogue(dialogue.dialogues);
    }

    void Update()
    {
        
    }
}
