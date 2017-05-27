using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private int currentScore = 0;
    private int currentNumLife = 1;
    public int scoreStarEnemy = 100;
    public int scorePolygonEnemy = 5;
    public int scoreTriangleEnemy = 10;

    public Text scoreCount;
    public Image[] numLife;

    public int CurrentNumLife
    {
        get
        {
            return currentNumLife;
        }
    }

    // Use this for initialization
    void Start ()
    {
        currentNumLife = 1;
        currentScore = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void ScoreAdd(int score)
    {
        currentScore += score;
        scoreCount.text = currentScore.ToString();
    }

    public void LifeAdd()
    {
        if (currentNumLife < 3)
        {
            numLife[currentNumLife].enabled = true;

            currentNumLife++;

            PoolManager.ReleaseObject(PoolManager.Instance.listPrefab[11].gameObject);
            PoolManager.SpawnObject(PoolManager.Instance.listPrefab[11].gameObject, Vector3.zero, Quaternion.identity);
        }
    }

    public void LifeDel()
    {
        if (currentNumLife > 0)
        {
            currentNumLife--;

            numLife[currentNumLife].enabled = false;

            if (currentNumLife == 0)
            {
                numLife[0].enabled = false;
                GameOver();
            }

            // Setup SoundEffect EarthHit
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.earthHit);

            PoolManager.ReleaseObject(PoolManager.Instance.listPrefab[11].gameObject);
            PoolManager.SpawnObject(PoolManager.Instance.listPrefab[11].gameObject, Vector3.zero, Quaternion.identity);
        }
    }

    public void GameOver()
    {
        var explosion = PoolManager.SpawnObject(PoolManager.Instance.listPrefab[12], Vector3.zero, Quaternion.identity);
        HideAll(Vector3.zero);
        PoolManager.Instance.StartCoroutine(ReturnPool(explosion.gameObject, 4));

        PoolManager.Instance.StartCoroutine(ShowDialogGameOver());

        PlayerManager.Instance.gameObject.SetActive(false);

        //Setup SoundEffect EarthDie
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.earthDie);
    }

    IEnumerator ShowDialogGameOver()
    {
        yield return new WaitForSeconds(2);

        SaveBestScore(currentScore, AudioManager.Instance.GetIndexCurrentLv);
        GA_FREE_Demo02.Instance.ShowDialog();
        GameOverManager.Instance.ShowDialog();
    }

    void SaveBestScore(int score, int indexLv)
    {
        string key = "BestScore" + indexLv.ToString();

        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetInt(key) < score)
                PlayerPrefs.SetInt(key, score);
        }
        else
        {
            PlayerPrefs.SetInt(key, score);
        }

        PlayerPrefs.Save();
    }

    IEnumerator ReturnPool(GameObject gameObj, float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.ReleaseObject(gameObj);
    }


    public void SuckInBlackHole()
    {
        var blackHole = GameObject.FindGameObjectWithTag("BlackHoleKillKing");
        HideAll(blackHole.transform.position);

        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.earthSuckedIntoBH);

        PoolManager.Instance.StartCoroutine(ShowDialogGameOver());
    }

    void HideAll(Vector3 position)
    {
        KingBehaviour.Instance.coll.enabled = false;
        KingBehaviour.Instance.transform.DOMove(position, 2);
        KingBehaviour.Instance.transform.DOScale(Vector3.zero, 2);

        var shield = GameObject.FindGameObjectWithTag("Shield");
        shield.transform.DOMove(position, 2);
        shield.transform.DOScale(Vector3.zero, 2);

        // Never use prefab enabled = false;
        PlayerManager.Instance.player.transform.DOMove(position, 2);
        PlayerManager.Instance.player.transform.DOScale(Vector3.zero, 2);
        PlayerManager.Instance.player.gameObject.SetActive(false);


        SpectrumCircle.Instance.transform.DOMove(position, 2);
        SpectrumCircle.Instance.transform.DOScale(Vector3.zero, 2);

        AudioMeasure.Instance.GetComponent<AudioSource>().DOFade(0, 2);
    }
}
