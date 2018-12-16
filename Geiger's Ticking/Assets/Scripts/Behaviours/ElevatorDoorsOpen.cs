using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorsOpen : MonoBehaviour {

    [SerializeField] private Animator _anim;

	void Start ()
    {
        _anim.SetTrigger("Interact");
	}
}
