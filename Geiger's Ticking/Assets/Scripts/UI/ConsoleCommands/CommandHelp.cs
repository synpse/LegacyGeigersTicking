﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHelp : ConsoleCommand
{

    // Overrides
    public override string Name { get; protected set; }

    public override string Command { get; protected set; }

    // Noclip command
    public CommandHelp()
    {
        Name = "help";
        Command = "help";

        AddCommandToConsole();
    }

    public override void RunCommand()
    {
        Debug.Log("List of available commands:");
        Debug.Log("help - " +
            "Shows available commands and their usages.");
        Debug.Log("quit - " +
            "Force quits the application without saving.");
        Debug.Log("clear - " +
            "Clears the console.");
        Debug.Log("showFps - " +
            "Shows frames per second.");
        Debug.Log("hideFps - " +
            "Hides frames per second.");
        Debug.Log("noClip - " +
            "Tells player controller to ignore gravity and collision detection.");
        Debug.Log("clip - " +
            "Tells player controller to reapply gravity and collision detection.");
        Debug.Log("newGame - " +
            "Loads a new game.");
        Debug.Log("newTestingScene - " +
            "Loads a new TestingScene.");
        Debug.Log("mainMenu - " +
            "Returns to Main Menu.");
        Debug.Log("showRads - " +
            "Shows accumulated rads.");
    }

    public static CommandHelp CreateCommand()
    {
        // Returns command
        return new CommandHelp();
    }
}
