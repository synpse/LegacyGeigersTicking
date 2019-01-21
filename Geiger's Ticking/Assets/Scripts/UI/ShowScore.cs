using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public GameObject panel;
    public Text scoreText;

    LevelChanger levelChanger;

    void Start ()
    {
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        panel.SetActive(false);
        ShowEnd();
    }

    public void ShowEnd()
    {
        panel.SetActive(true);
        scoreText.text =
            $"You successfully left the powerplant. " +
            $"After escaping, you lived forever happy. " +
            $"By forever I mean for " +
            $"{Math.Floor(27375 / ActivateRadEffect.radsAccumulated)} days.";

        // 27375 is the number of days the average individual has to live

        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(10);
        levelChanger.FadeToLevel(1);
    }
}
