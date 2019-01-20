using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndHere : MonoBehaviour
{
    LevelChanger levelChanger;

    private void Start()
    {
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
    }

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
        levelChanger.FadeToLevel(0);
    }
}
