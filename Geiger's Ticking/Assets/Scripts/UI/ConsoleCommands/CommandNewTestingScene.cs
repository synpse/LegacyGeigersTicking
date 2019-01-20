using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandNewTestingScene : ConsoleCommand
{
    // Overrides
    public override string Name { get; protected set; }

    public override string Command { get; protected set; }

    LevelChanger levelChanger;

    // Noclip command
    public CommandNewTestingScene()
    {
        Name = "newTestingScene";
        Command = "newTestingScene";

        AddCommandToConsole();
    }

    public override void RunCommand()
    {
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        levelChanger.FadeToLevel(2);
        Debug.Log("Loading TestingScene... ");
    }

    public static CommandNewTestingScene CreateCommand()
    {
        // Returns command
        return new CommandNewTestingScene();
    }
}