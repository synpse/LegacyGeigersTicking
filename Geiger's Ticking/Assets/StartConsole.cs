using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartConsole : MonoBehaviour {

    public GameObject _consoleScreen;
    public GameObject _console;
    public GameObject _player;
    public Animator _animator;
    public Camera _camera1;
    public Camera _camera2;

    private Interactible console;

    private bool active;

    void Start ()
    {
        console = _console.gameObject.GetComponent<Interactible>();
        _consoleScreen.SetActive(false);
        _camera1.enabled = true;
        _camera2.enabled = false;
    }
	
	void Update ()
    {
        if (console.interactiveOn)
        {
            StartCoroutine(ToggleScreen());
            _camera1.enabled = false;
            _camera2.enabled = true;
            _animator.SetTrigger("Zoom");
            console.interactiveOn = false;
        }

        // If console is active
        if (_consoleScreen.gameObject.activeInHierarchy == true && active == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _player.GetComponent<RigidbodyFirstPersonController>().enabled = false;
        }
        if (_consoleScreen.gameObject.activeInHierarchy == false && active == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
            _animator.SetTrigger("Zoom");
            StartCoroutine(CloseScreen());
            active = false;
        }
    }

    IEnumerator ToggleScreen()
    {
        yield return new WaitForSeconds(0.75f);
        _consoleScreen.SetActive(true);
        active = true;
        FindObjectOfType<DialogueTrigger>().TriggerDialogue();
    }

    IEnumerator CloseScreen()
    {
        yield return new WaitForSeconds(1f);
        _camera1.enabled = true;
        _camera2.enabled = false;
    }
}
