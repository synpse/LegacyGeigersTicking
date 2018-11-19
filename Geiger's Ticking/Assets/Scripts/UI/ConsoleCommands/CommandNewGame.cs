using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Console
{
    public class CommandNewGame : ConsoleCommand
    {
        // Overrides
        public override string Name { get; protected set; }

        public override string Command { get; protected set; }

        // Noclip command
        public CommandNewGame()
        {
            Name = "newGame";
            Command = "newGame";

            AddCommandToConsole();
        }

        public override void RunCommand()
        {
            SceneManager.LoadScene("MainScene");
            Debug.Log("Loading MainScene... ");
        }

        public static CommandNewGame CreateCommand()
        {
            // Returns command
            return new CommandNewGame();
        }
    }
}