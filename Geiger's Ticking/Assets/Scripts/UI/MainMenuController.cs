using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    void Update () {
        // If "\" is pressed and game is not paused
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            // On keypress turn on or off depending on context
            // activeInHierarchy returns a bool with active or not active
            // If ON => OFF, if OFF => ON
            DeveloperConsole.Instance.consoleCanvas.gameObject.SetActive
            (!DeveloperConsole.Instance.consoleCanvas.gameObject.activeInHierarchy);
        }

        // If active
        if (DeveloperConsole.Instance.consoleCanvas.gameObject.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
