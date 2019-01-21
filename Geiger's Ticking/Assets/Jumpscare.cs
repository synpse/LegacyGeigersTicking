using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;

    bool stop;

    void Awake ()
    {
        _sound.Stop();

        stop = false;
    }
	
	void Update ()
    {
		if (gameObject.GetComponent<Interactible>().interactiveOn == true && stop == false)
        {
            _sound.Play();
            StartCoroutine(Stop());
            stop = true;
        }
	}

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(9);
        _sound.Stop();
    }
}
