using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ActivateRadEffect : MonoBehaviour {

    // Header for the sake of organization
    [Header("Components")]

    [SerializeField] private AudioSource sound;
    [SerializeField] private PostProcessingProfile ppp;
    [SerializeField] private SphereCollider RadiationLow;
    [SerializeField] private SphereCollider RadiationMedium;
    [SerializeField] private SphereCollider RadiationHigh;
    [SerializeField] private SphereCollider RadiationExtreme;

    [HideInInspector] public static float radsAccumulated;

    GrainModel.Settings grain;
    ChromaticAberrationModel.Settings aberration;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            sound.Play();
            sound.loop = true;

            if (gameObject.tag == "RadZoneLow")
            {
                grain.intensity = 0.3f;
                grain.size = 1.4f;
                aberration.intensity = 0.25f;
                ppp.grain.settings = grain;
                ppp.chromaticAberration.settings = aberration;
                Debug.Log("Radiation intensity: LOW");
            }
            if (gameObject.tag == "RadZoneMedium")
            {
                RadiationLow.enabled = false;
                grain.intensity = 0.5f;
                grain.size = 1.6f;
                aberration.intensity = 0.55f;
                ppp.grain.settings = grain;
                ppp.chromaticAberration.settings = aberration;
                Debug.Log("Radiation intensity: MEDIUM");
            }
            if (gameObject.tag == "RadZoneHigh")
            {
                RadiationLow.enabled = false;
                RadiationMedium.enabled = false;
                grain.intensity = 0.7f;
                grain.size = 1.8f;
                aberration.intensity = 0.85f;
                ppp.grain.settings = grain;
                ppp.chromaticAberration.settings = aberration;
                Debug.Log("Radiation intensity: HIGH");
            }
            if (gameObject.tag == "RadZoneExtreme")
            {
                RadiationLow.enabled = false;
                RadiationMedium.enabled = false;
                RadiationHigh.enabled = false;
                grain.intensity = 1f;
                grain.size = 2f;
                aberration.intensity = 1f;
                ppp.grain.settings = grain;
                ppp.chromaticAberration.settings = aberration;
                Debug.Log("Radiation intensity: EXTREME");
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (gameObject.tag == "RadZoneLow")
            {
                radsAccumulated += Random.Range(0.0f, 0.25f);
            }
            if (gameObject.tag == "RadZoneMedium")
            {
                radsAccumulated += Random.Range(0.0f, 0.5f);
            }
            if (gameObject.tag == "RadZoneHigh")
            {
                radsAccumulated += Random.Range(0.0f, 0.75f);
            }
            if (gameObject.tag == "RadZoneExtreme")
            {
                radsAccumulated += Random.Range(0.0f, 1f);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            sound.Stop();
            RadiationLow.enabled = true;
            RadiationMedium.enabled = true;
            RadiationHigh.enabled = true;
            grain.intensity = 0f;
            ppp.grain.settings = grain;
            aberration.intensity = 0f;
            ppp.chromaticAberration.settings = aberration;
        }
    }

    private void Start()
    {
        sound.Stop();
        grain = ppp.grain.settings;
        aberration = ppp.chromaticAberration.settings;

        // Prevents bug in editor
        grain.intensity = 0f;
        ppp.grain.settings = grain;
        aberration.intensity = 0f;
        ppp.chromaticAberration.settings = aberration;
    }
}

