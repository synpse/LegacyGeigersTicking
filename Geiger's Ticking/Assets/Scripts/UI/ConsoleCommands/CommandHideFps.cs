using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideFps : ConsoleCommand
{
    // Overrides
    public override string Name { get; protected set; }

    public override string Command { get; protected set; }

    // Noclip command
    public HideFps()
    {
        Name = "hideFps";
        Command = "hideFps";

        AddCommandToConsole();
    }

    public override void RunCommand()
    {
        DisplayFPS.style.normal.textColor = new Color(255f, 255f, 255f, 0.0f);
    }

    public static HideFps CreateCommand()
    {
        // Returns command
        return new HideFps();
    }
}
