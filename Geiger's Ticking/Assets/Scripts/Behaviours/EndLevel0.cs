using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel0 : MonoBehaviour
{
    private bool loading;

    LevelChanger levelChanger;

    private void Start()
    {
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
    }

    void Update ()
    {

        if (gameObject.GetComponent<Interactible>().interactiveOn == true 
            && loading == false)
        {
            StartCoroutine("Load");
            loading = true;
        }
	}

    IEnumerator Load()
    {
        yield return new WaitForSeconds(3);
        levelChanger.FadeToLevel(4);
    }
}
