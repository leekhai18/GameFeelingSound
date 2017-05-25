using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : Singleton<GameOverManager>
{
    public Text scoreCount;
    public Text bestScore;
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ShowDialog()
    {
        PoolManager.Instance.StartCoroutine(CounterScore(Convert.ToInt32(GameManager.Instance.scoreCount.text)));
        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
    }

    IEnumerator CounterScore(int score)
    {
       
        for (int i = 0; i < score + 1; i += 5)
        {
            yield return new WaitForSecondsRealtime(5.0f / score);
            scoreCount.text = i.ToString();
        }
    }


}
