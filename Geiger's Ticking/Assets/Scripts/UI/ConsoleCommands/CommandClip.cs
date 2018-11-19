using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console
{
    public class CommandClip : ConsoleCommand
    {
        // Header for the sake of organization
        [Header("Player Components")]
        public Rigidbody player;

        // Overrides
        public override string Name { get; protected set; }

        public override string Command { get; protected set; }

        // Clip command
        public CommandClip()
        {
            Name = "clip";
            Command = "clip";

            AddCommandToConsole();
        }

        public override void RunCommand()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
            player.useGravity = true;
            player.detectCollisions = true;
            player.isKinematic = false;
            Debug.Log("Gravity and collision detection are now enabled. ");
        }

        public static CommandClip CreateCommand()
        {
            // Returns command
            return new CommandClip();
        }
    }
}