using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRadEffect : MonoBehaviour {

    [Header("Components")]
    public AudioSource sound;
    private bool triggerOn = false;
    private float radsAccumulated;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            sound.Play();
            sound.loop = true;
            if (gameObject.tag == "RadZoneLow")
            {
                Debug.Log("Max Radiation intensity: 2.5f (LOW)");
                Debug.Log("Radiation Accumulation Index: 0f - 2.5f");
            }
            if (gameObject.tag == "RadZoneMedium")
            {
                Debug.Log("Max Radiation intensity: 5.0f (MEDIUM)");
                Debug.Log("Radiation Accumulation Index: 2.5f - 5f");
            }
            if (gameObject.tag == "RadZoneHigh")
            {
                Debug.Log("Max Radiation intensity: 7.5f (HIGH)");
                Debug.Log("Radiation Accumulation Index: 5f - 7.5f");
            }
            if (gameObject.tag == "RadZoneExtreme")
            {
                Debug.Log("Max Radiation intensity: 10.0f (EXTREME)");
                Debug.Log("Radiation Accumulation Index: 7.5f - 10f");
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            radsAccumulated += Random.Range(0f, 10f) * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            sound.Stop();
            Debug.Log("Rads Accumulated: " + radsAccumulated);
        }
    }

    private void Start()
    {
        sound.Stop();
    }
}
