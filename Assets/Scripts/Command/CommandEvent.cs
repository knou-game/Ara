using UnityEngine;

[System.Serializable]
public class CommandEvent
{
    public string name;
    public int scene;
    public CommandBase[] commands;
}