using UnityEngine;

public class IntegrationController : SingletonPersistent<CommandDatabase>
{
    [SerializeField]
    CommandEvent command;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        ScenarioManager.Instance.commands = CommandDatabase.Instance.GetCommands(command.scene);
        ScenarioManager.Instance.StartScenario();
    }

    void Update()
    {

    }
}
