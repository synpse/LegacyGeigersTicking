using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject objLever1;
    public GameObject objLever2;
    public GameObject objLever3;
    public GameObject objElevatorPowerButton;
    public GameObject unknownEntity;

    private Interactible lever1;
    private Interactible lever2;
    private Interactible lever3;
    private Interactible elevatorPowerButton;

    private bool ended;
    private bool interacted;

    private void Start()
    {
        lever1 = objLever1.gameObject.GetComponent<Interactible>();
        lever2 = objLever2.gameObject.GetComponent<Interactible>();
        lever3 = objLever3.gameObject.GetComponent<Interactible>();
        elevatorPowerButton = objElevatorPowerButton.gameObject.GetComponent<Interactible>();
        unknownEntity.GetComponent<Renderer>().enabled = false;
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
            unknownEntity.GetComponent<Renderer>().enabled = true;
            Debug.Log("Puzzle completed!");
            interacted = true;
        }
    }
}