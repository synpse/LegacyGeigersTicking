using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [Header("Components")]
    public Canvas pauseMenuCanvas;

    void Start ()
    {
        // Disable console at start
        pauseMenuCanvas.gameObject.SetActive(false);
    }
	
	void Update ()
    {

    }
}
