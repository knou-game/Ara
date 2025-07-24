using UnityEngine;

public class IntegrationController : MonoBehaviour
{
    [SerializeField]
    CommandEvent command;

    void Start()
    {
        Debug.Log("Why");
        ScenarioManager.Instance.commands = CommandDatabase.Instance.GetCommands(command.scene);
        ScenarioManager.Instance.StartScenario();
    }

    void Update()
    {

    }
}
