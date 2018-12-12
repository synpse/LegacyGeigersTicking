using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject _objLever1;
    public GameObject _objLever2;
    public GameObject _objLever3;
    public GameObject _objElevatorPowerButton;

    private Interactible lever1;
    private Interactible lever2;
    private Interactible lever3;
    private Interactible elevatorPowerButton;

    private bool ended;
    private bool interacted;

    private void Start()
    {
        lever1 = _objLever1.gameObject.GetComponent<Interactible>();
        lever2 = _objLever2.gameObject.GetComponent<Interactible>();
        lever3 = _objLever3.gameObject.GetComponent<Interactible>();
        elevatorPowerButton = _objElevatorPowerButton.gameObject.GetComponent<Interactible>();
    }

    private void Update()
    {
        if (!ended)
        {
            Check();
        }

        if (!interacted)
        {
            PowerButtonListener();
        }
    }

    private void Check()
    {
        if ((lever1.interactiveOn == false) &&
            (lever2.interactiveOn == true) &&
            (lever3.interactiveOn == true))
        {
            elevatorPowerButton.isActive = true;
            elevatorPowerButton.isInteractive = true;
        }
        else
        {
            elevatorPowerButton.isActive = false;
            elevatorPowerButton.isInteractive = false;
        }
    }

    private void PowerButtonListener()
    {
        if (elevatorPowerButton.interactiveOn == true)
        {
            ended = true;
            // Force deactivation
            elevatorPowerButton.isActive = false;
            elevatorPowerButton.isInteractive = false;
            Debug.Log("Puzzle completed!");
            interacted = true;
        }
    }
}