using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandHelp : ConsoleCommand
    {
        // Header for the sake of organization
        [Header("Player Components")]
        public Rigidbody player;

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
            Debug.Log("noclip - " +
                "Tells player controller to ignore gravity and collision detection.");
            Debug.Log("clip - " +
                "Tells player controller to reapply gravity and collision detection.");
            Debug.Log("quit - " +
                "Force quits the application without saving.");
        }

        public static CommandHelp CreateCommand()
        {
            // Returns command
            return new CommandHelp();
        }
    }
}
