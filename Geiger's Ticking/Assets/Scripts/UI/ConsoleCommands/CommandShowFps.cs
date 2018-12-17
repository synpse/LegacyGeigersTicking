using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFps : ConsoleCommand
{
    // Overrides
    public override string Name { get; protected set; }

    public override string Command { get; protected set; }

    // Noclip command
    public ShowFps()
    {
        Name = "showFps";
        Command = "showFps";

        AddCommandToConsole();
    }

    public override void RunCommand()
    {
        DisplayFPS.style.normal.textColor = new Color(255f, 255f, 255f, 1.0f);
    }

    public static ShowFps CreateCommand()
    {
        // Returns command
        return new ShowFps();
    }
}
