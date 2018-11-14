using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Console
{
    public class CommandClear : ConsoleCommand
    {
        public static bool clear;

        // Overrides
        public override string Name { get; protected set; }

        public override string Command { get; protected set; }

        // Noclip command
        public CommandClear()
        {
            Name = "clear";
            Command = "clear";

            AddCommandToConsole();
        }

        public override void RunCommand()
        {
            Debug.Log("Clearing console...");
            clear = false;
            clear = !clear;
            Debug.Log("Type 'help' to see available commands.");
        }

        public static CommandClear CreateCommand()
        {
            // Returns command
            return new CommandClear();
        }
    }
}
