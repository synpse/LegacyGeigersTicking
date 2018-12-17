using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandNewTestingScene : ConsoleCommand
{
    // Overrides
    public override string Name { get; protected set; }

    public override string Command { get; protected set; }

    // Noclip command
    public CommandNewTestingScene()
    {
        Name = "newTestingScene";
        Command = "newTestingScene";

        AddCommandToConsole();
    }

    public override void RunCommand()
    {
        LevelChanger.Instance.FadeToLevel(2);
        Debug.Log("Loading TestingScene... ");
    }

    public static CommandNewTestingScene CreateCommand()
    {
        // Returns command
        return new CommandNewTestingScene();
    }
}