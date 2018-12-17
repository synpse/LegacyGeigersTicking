using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHere : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Ending Test Play...");
            StartCoroutine(Load());
        }
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(3);
        LevelChanger.Instance.FadeToLevel(0);
    }
}
