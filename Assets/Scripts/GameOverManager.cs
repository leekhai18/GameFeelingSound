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
        PoolManager.Instance.StartCoroutine(AnimationCountScore(Convert.ToInt32(GameManager.Instance.scoreCount.text)));
    }

    IEnumerator AnimationCountScore(int score)
    {
        int best = PlayerPrefs.GetInt("BestScore" + AudioManager.Instance.GetIndexCurrentLv.ToString());

        if (score == 0)
        {
            scoreCount.text = "0";
            bestScore.text = best.ToString();
        }
        else
        {
            if (score < best)
            {
                bestScore.text = best.ToString();

                for (int i = 0; i < score + 1; i += 5)
                {
                    yield return new WaitForSecondsRealtime(5.0f / score);
                    scoreCount.text = i.ToString();
                }
            }
            else
            {
                for (int i = 0; i < score + 1; i += 5)
                {
                    yield return new WaitForSecondsRealtime(5.0f / score);
                    scoreCount.text = i.ToString();
                }

                bestScore.text = best.ToString();
            }
        }
    }


}
