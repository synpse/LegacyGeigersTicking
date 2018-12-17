using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDoorOpen : MonoBehaviour {

    public Rigidbody _ragdoll;
    private bool opened;

	private void Start ()
    {
        _ragdoll.isKinematic = true;
    }
	
	private void Update ()
    {
		if (gameObject.GetComponent<Interactible>().interactiveOn == true
            && opened == false)
        {
            _ragdoll.isKinematic = false;
            opened = true;
        }
	}
}
