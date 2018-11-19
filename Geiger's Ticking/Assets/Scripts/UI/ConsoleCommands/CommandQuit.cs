using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandQuit : ConsoleCommand
    {
        // Overrides
        public override string Name { get; protected set; }

        public override string Command { get; protected set; }

        // Quit command
        public CommandQuit()
        {
            Name = "quit";
            Command = "quit";

            AddCommandToConsole();
        }

        public override void RunCommand()
        {
            // If is aditor
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                // Stop
                UnityEditor.EditorApplication.isPlaying = false;
                Debug.Log("Quit in editor mode successfull. ");
#endif
            }
            // If is game
            else
            {
                // Quit
                Application.Quit();
                Debug.Log("Quit successfull.");
            }
        }

        public static CommandQuit CreateCommand()
        {
            // Returns command
            return new CommandQuit();
        }
    }
}
