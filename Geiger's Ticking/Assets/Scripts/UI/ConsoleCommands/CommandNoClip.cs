using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandNoClip : ConsoleCommand
{
    // Header for the sake of organization
    [Header("Player Components")]
    public Rigidbody player;

    // Overrides
    public override string Name { get; protected set; }

    public override string Command { get; protected set; }

    // Noclip command
    public CommandNoClip()
    {
        Name = "noClip";
        Command = "noClip";

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

    public static CommandNoClip CreateCommand()
    {
        // Returns command
        return new CommandNoClip();
    }
}