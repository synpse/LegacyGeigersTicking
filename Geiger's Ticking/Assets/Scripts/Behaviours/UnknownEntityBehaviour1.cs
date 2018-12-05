using System.Collections;
using UnityEngine;
using AuraAPI;

public class UnknownEntityBehaviour1 : MonoBehaviour {

    public Camera _camera;
    private RaycastHit _raycastHit;

    public GameObject _unknownEntity;
    private bool ended;

    public Light _light;
	
	void Update ()
    {
        if (!ended)
        {
            CheckForUnknownEntity();
        }
    }

    private void CheckForUnknownEntity()
    {
        if (_unknownEntity.GetComponent<Renderer>().enabled == true)
        {
            if (Physics.Raycast(_camera.transform.position,
            _camera.transform.forward, out _raycastHit, 1000f))
            {
                if (_raycastHit.collider.gameObject.name == "unknown")
                {
                    Debug.Log($"Hit with {_raycastHit.collider} with distance {_raycastHit.distance}");
                    StartCoroutine("DespawnUnknownEntity");
                    StartCoroutine("LightFlashOn");
                    ended = true;
                }
            }
        }
    }

    IEnumerator DespawnUnknownEntity()
    {
        yield return new WaitForSeconds(2f);
        _unknownEntity.GetComponent<Renderer>().enabled = false;
    }

    IEnumerator LightFlashOn()
    {
        yield return new WaitForSeconds(1.8f);
        _light.intensity = 0f;
        StartCoroutine("LightFlashOff");
    }

    IEnumerator LightFlashOff()
    {
        yield return new WaitForSeconds(0.20f);
        _light.intensity = 1f;
    }
}