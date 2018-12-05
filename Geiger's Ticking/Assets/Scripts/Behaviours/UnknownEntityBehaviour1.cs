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
                    StartCoroutine("LightOff");
                    ended = true;
                }
            }
        }
    }

    IEnumerator LightOff()
    {
        yield return new WaitForSeconds(0.25f);
        _light.intensity = 0f;
        StartCoroutine("LightOn");
    }

    IEnumerator LightOn()
    {
        yield return new WaitForSeconds(1f);
        _unknownEntity.GetComponent<Renderer>().enabled = false;
        _light.intensity = 1f;
    }
}