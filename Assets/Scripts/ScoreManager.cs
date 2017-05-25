using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public float currentScore = 0;
    public int currentNumLife = 1;
    public int scoreStarEnemy = 100;
    public int scorePolygonEnemy = 5;
    public int scoreTriangleEnemy = 10;

    bool isGameOver = true;

	// Use this for initialization
	void Start ()
    {
        isGameOver = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentNumLife == 0 && isGameOver == true)
        {
            GameManager.Instance.GameOver();
            isGameOver = false;
        }
    }


}
