using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    DeveloperConsole developerConsole;

    private void Start()
    {
        developerConsole = GameObject.Find("DeveloperConsole").GetComponent<DeveloperConsole>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update () {
        // If "\" is pressed and game is not paused<
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            // On keypress turn on or off depending on context
            // activeInHierarchy returns a bool with active or not active
            // If ON => OFF, if OFF => ON
            developerConsole.consoleCanvas.gameObject.SetActive
            (!developerConsole.consoleCanvas.gameObject.activeInHierarchy);
        }

        // If active
        if (developerConsole.consoleCanvas.gameObject.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
