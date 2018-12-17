using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandMainMenu : ConsoleCommand
{
    // Overrides
    public override string Name { get; protected set; }

    public override string Command { get; protected set; }

    // Noclip command
    public CommandMainMenu()
    {
        Name = "mainMenu";
        Command = "mainMenu";

        AddCommandToConsole();
    }

    public override void RunCommand()
    {
        LevelChanger.Instance.FadeToLevel(0);
        Debug.Log("Loading MainMenu... ");
    }

    public static CommandMainMenu CreateCommand()
    {
        // Returns command
        return new CommandMainMenu();
    }
}