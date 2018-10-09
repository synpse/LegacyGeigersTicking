using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public void NewGame()
    {
        // Create New Game
        Debug.Log("Requesting new game...");
        Console.DeveloperConsole.Commands["newGame"].RunCommand();
    }

    public void Quit()
    {
        // Quit through console
        Debug.Log("Sending quit signal to console...");
        Console.DeveloperConsole.Commands["quit"].RunCommand();
    }

    public void MainMenu()
    {
        // Create New Game
        Debug.Log("Returning to main menu...");
        Console.DeveloperConsole.Commands["mainMenu"].RunCommand();
    }
}
