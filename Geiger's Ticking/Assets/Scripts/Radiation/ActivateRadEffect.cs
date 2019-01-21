using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ActivateRadEffect : MonoBehaviour {

    // Header for the sake of organization
    [Header("Components")]

    [SerializeField] private PostProcessingProfile _ppp;
    [SerializeField] private GameObject _player;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private AudioClip _radLow;
    [SerializeField] private AudioClip _radMed;
    [SerializeField] private AudioClip _radHigh;
    [SerializeField] private AudioClip _radExtr;

    [HideInInspector] public static float radsAccumulated;

    private float radIntensity;
    private float radZoneRadius;

    private bool inRadZoneLow;
    private bool inRadZoneMed;
    private bool inRadZoneHigh;
    private bool inRadZoneExtr;

    private RigidbodyFirstPersonController player;

    private GrainModel.Settings _grain;
    private ChromaticAberrationModel.Settings _aberration;

    private void Awake()
    {
        _sound.loop = true;
        _sound.Stop();
        _grain = _ppp.grain.settings;
        _aberration = _ppp.chromaticAberration.settings;

        // Prevents bug in editor
        _grain.intensity = 0f;
        _aberration.intensity = 0f;
        _ppp.grain.settings = _grain;
        _ppp.chromaticAberration.settings = _aberration;
    }

    private void Start()
    {
        player = _player.GetComponent<RigidbodyFirstPersonController>();

        // Start at one to prevent bug on game end
        radsAccumulated = 1;
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (gameObject.tag == "RadZone")
            {
                radZoneRadius = (gameObject.transform.position - _player.transform.position).magnitude;

                // The feedback should decrease with distance from the center.
                radIntensity = 1.2f / radZoneRadius;

                _grain.intensity = radIntensity * 2f;
                _grain.size = 2f;
                _aberration.intensity = 0;
                _ppp.grain.settings = _grain;
                _ppp.chromaticAberration.settings = _aberration;

                if (radIntensity > 0f && radIntensity <= 0.2f)
                {
                    radsAccumulated += Random.Range(0f, 0.2f);

                    player.movementSettings.ForwardSpeed = 2f;
                    player.movementSettings.BackwardSpeed = 1f;
                    player.movementSettings.StrafeSpeed = 1f;

                    inRadZoneMed = false;
                    inRadZoneHigh = false;
                    inRadZoneExtr = false;

                    if (inRadZoneLow == false)
                    {
                        _sound.clip = _radLow;
                        _sound.Play();
                        inRadZoneLow = true;
                    }
                }

                if (radIntensity > 0.2f && radIntensity <= 0.4f)
                {
                    radsAccumulated += Random.Range(0.2f, 0.4f);

                    player.movementSettings.ForwardSpeed = 1f;
                    player.movementSettings.BackwardSpeed = 0.5f;
                    player.movementSettings.StrafeSpeed = 0.5f;

                    inRadZoneLow = false;
                    inRadZoneHigh = false;
                    inRadZoneExtr = false;

                    if (inRadZoneMed == false)
                    {
                        _sound.clip = _radMed;
                        _sound.Play();
                        inRadZoneMed = true;
                    }
                }

                if (radIntensity > 0.4f && radIntensity <= 0.6f)
                {
                    radsAccumulated += Random.Range(0.4f, 0.6f);

                    player.movementSettings.ForwardSpeed = 0.5f;
                    player.movementSettings.BackwardSpeed = 0.25f;
                    player.movementSettings.StrafeSpeed = 0.25f;

                    inRadZoneLow = false;
                    inRadZoneMed = false;
                    inRadZoneExtr = false;

                    if (inRadZoneHigh == false)
                    {
                        _sound.clip = _radHigh;
                        _sound.Play();
                        inRadZoneHigh = true;
                    }
                }

                if (radIntensity > 0.6f)
                {
                    radsAccumulated += Random.Range(0.6f, 1f);

                    player.movementSettings.ForwardSpeed = 0.25f;
                    player.movementSettings.BackwardSpeed = 0.1f;
                    player.movementSettings.StrafeSpeed = 0.1f;

                    inRadZoneLow = false;
                    inRadZoneMed = false;
                    inRadZoneHigh = false;

                    if (inRadZoneExtr == false)
                    {
                        _sound.clip = _radExtr;
                        _sound.Play();
                        inRadZoneExtr = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            inRadZoneLow = false;
            inRadZoneMed = false;
            inRadZoneHigh = false;
            inRadZoneExtr = false;
            _sound.Stop();
            _grain.intensity = 0f;
            _aberration.intensity = 0f;
            _ppp.grain.settings = _grain;
            _ppp.chromaticAberration.settings = _aberration;
            player.movementSettings.ForwardSpeed = 3f;
            player.movementSettings.BackwardSpeed = 2f;
            player.movementSettings.StrafeSpeed = 2f;
        }
    }
}

