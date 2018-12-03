using UnityEngine;
using AuraAPI;

public class Puzzle : MonoBehaviour
{
    public GameObject objLever1;
    public GameObject objLever2;
    public GameObject objLever3;
    public GameObject objElevatorPowerButton;
    private Interactible lever1;
    private Interactible lever2;
    private Interactible lever3;
    private Interactible elevatorPowerButton;
    private bool ended;

    private void Start()
    {
        lever1 = objLever1.gameObject.GetComponent<Interactible>();
        lever2 = objLever2.gameObject.GetComponent<Interactible>();
        lever3 = objLever3.gameObject.GetComponent<Interactible>();
        elevatorPowerButton = objElevatorPowerButton.gameObject.GetComponent<Interactible>();
    }

    private void Update()
    {
        if (!ended)
        {
            Check();
        }
    }

    private void Check()
    {
        if ((lever1.interactiveOn == false) &&
            (lever2.interactiveOn == true) &&
            (lever3.interactiveOn == true))
        {
            elevatorPowerButton.Activate();
            elevatorPowerButton.isInteractive = true;
            Debug.Log("Puzzle completed!");
            ended = true;
        }
    }
}