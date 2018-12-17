using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartConsole : MonoBehaviour {

    public GameObject _consoleScreen;

    public GameObject _console;

    private Interactible console;

    public GameObject _player;

    private bool ended;

    void Start ()
    {
        console = _console.gameObject.GetComponent<Interactible>();
        _consoleScreen.SetActive(false);
    }
	
	void Update ()
    {
        if (!ended)
        {
            Check();
        }

        // If console is active
        if (_consoleScreen.gameObject.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _player.GetComponent<RigidbodyFirstPersonController>().enabled = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
            ended = false;
            console.interactiveOn = false;
        }
    }

    private void Check()
    {
        if (console.interactiveOn == true)
        {
            _consoleScreen.SetActive(true);
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            ended = true;
        }
    }
}
