using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRads : ConsoleCommand
{
    // Overrides
    public override string Name { get; protected set; }

    public override string Command { get; protected set; }

    // Noclip command
    public ShowRads()
    {
        Name = "showRads";
        Command = "showRads";

        AddCommandToConsole();
    }

    public override void RunCommand()
    {
        Debug.Log("Rads Accumulated: " + ActivateRadEffect.radsAccumulated);
    }

    public static ShowRads CreateCommand()
    {
        // Returns command
        return new ShowRads();
    }
}
