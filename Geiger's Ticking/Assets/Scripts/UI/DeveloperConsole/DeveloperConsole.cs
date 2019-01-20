using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class ConsoleCommand
{
    // Foundation for commands
    public abstract string Name { get; protected set; }

    public abstract string Command { get; protected set; }

    public void AddCommandToConsole()
    {
        DeveloperConsole.AddCommandsToConsole(Command, this);
    }

    public abstract void RunCommand();
}

public class DeveloperConsole : MonoBehaviour
{
    // Dictionary
    public static Dictionary<string, ConsoleCommand> Commands { get; set; }

    // Header for the sake of organization
    [Header("UI Components")]

    public Canvas consoleCanvas;

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Text consoleText;
    [SerializeField] private Text inputText;
    [SerializeField] private InputField consoleInput;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        // Instantiate command
        Commands = new Dictionary<string, ConsoleCommand>();
    }

    private void CreateCommands()
    {
        CommandHelp.CreateCommand();
        CommandNoClip.CreateCommand();
        CommandClip.CreateCommand();
        CommandQuit.CreateCommand();
        CommandNewGame.CreateCommand();
        CommandNewTestingScene.CreateCommand();
        CommandMainMenu.CreateCommand();
        CommandClear.CreateCommand();
        ShowRads.CreateCommand();
        ShowFps.CreateCommand();
        HideFps.CreateCommand();
    }

    public static void AddCommandsToConsole(string name, ConsoleCommand command)
    {
        if (!Commands.ContainsKey(name))
        {
            Commands.Add(name, command);
        }
    }

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logMessage, string stackTrace, LogType type)
    {
        string tmpmessage = "[" + type.ToString() + "] " + logMessage;
        AddMessageToConsole(tmpmessage);
    }

    private void Start()
    {
        // Disable console at start
        consoleCanvas.gameObject.SetActive(false);
        // Create Commands
        CreateCommands();
        // Start message
        Debug.Log("Type 'help' to see available commands.");
    }

    private void Update()
    {
        // If active
        if (consoleCanvas.gameObject.activeInHierarchy)
        {
            consoleInput.ActivateInputField();

            // If return / enter is pressed
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (inputText.text != "")
                {
                    // Add command as message
                    AddMessageToConsole(inputText.text);
                    // Send command
                    ParseInput(inputText.text);
                    // Clear input field
                    consoleInput.text = "";
                }
            }

            if (CommandClear.clear == true)
            {
                consoleText.text = "";
                CommandClear.clear = !CommandClear.clear;
            }
        }
    }

    private void AddMessageToConsole(string msg)
    {
        // Every time there's a message, there's a paragraph
        consoleText.text += msg + "\n";
    }

    private void ParseInput(string input)
    {
        string[] tmpinput = input.Split(null);

        // If no arguments are detected
        if (tmpinput.Length == 0 || tmpinput == null)
        {
            Debug.LogWarning("No arguments detected.");
            return;
        }

        // If arguments are detected but are not recognized by the dictionary
        if (!Commands.ContainsKey(tmpinput[0]))
        {
            Debug.LogWarning("Command not recognized or not currently available.");
        }
        // If arguments are detected and recognized by the dictionary
        else
        {
            Commands[tmpinput[0]].RunCommand();
        }
    }
}
