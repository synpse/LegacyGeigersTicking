using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : Interactible
{
    public GameObject lever1;
    public GameObject lever2;
    public GameObject lever3;

    private void Start()
    {
        lever1 = GameObject.Find("lever1");
        lever2 = GameObject.Find("lever2");
        lever3 = GameObject.Find("lever3");

    }

    private void Update()
    {
        if ((lever1.interactiveOn = true) && (lever2.interactiveOn = true)
            && (lever3.interactiveOn = true))
        {
            interactiveOn = false;
        }
    }
}