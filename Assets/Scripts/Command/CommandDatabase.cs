using System.Collections.Generic;
using UnityEngine;

public class CommandDatabase : SingletonPersistent<CommandDatabase>
{
    private string csvFileName = "script_data";

    Dictionary<int, CommandClass> commandDic = new Dictionary<int, CommandClass>();
    public static bool isFinish = false;

    protected override void Awake()
    {
        base.Awake();
        CommandParser parser = GetComponent<CommandParser>();
        CommandClass[] commands = parser.Parse(csvFileName);

        for (int i = 0; i < commands.Length; i++)
        {
            commandDic.Add(i + 1, commands[i]);
        }
        isFinish = true;
    }

    public CommandClass GetCommands(int scene)
    {
        return commandDic[scene];
    }
}
