using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public void Quit()
    {
        // Quit through console
        Debug.Log("Sending quit signal to console...");
        Console.DeveloperConsole.Commands["quit"].RunCommand();
    }
}
