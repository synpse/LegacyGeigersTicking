using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandNoclip : ConsoleCommand
    {
        // Header for the sake of organization
        [Header("Player Components")]
        public Rigidbody player;

        // Overrides
        public override string Name { get; protected set; }

        public override string Command { get; protected set; }

        // Noclip command
        public CommandNoclip()
        {
            Name = "noclip";
            Command = "noclip";

            AddCommandToConsole();
        }

        public override void RunCommand()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
            player.useGravity = false;
            player.detectCollisions = false;
            player.isKinematic = true;
            Debug.Log("Gravity and collision detection are now disabled. ");
        }

        public static CommandNoclip CreateCommand()
        {
            // Returns command
            return new CommandNoclip();
        }
    }
}