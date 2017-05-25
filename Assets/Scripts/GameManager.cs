using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void SuckInBlackHole()
    {
        var blackHole = GameObject.FindGameObjectWithTag("BlackHoleKillKing");
        HideAll(blackHole.transform.position);
    }

    public void GameOver()
    {
        var explosion = PoolManager.SpawnObject(PoolManager.Instance.listPrefab[12], Vector3.zero, Quaternion.identity);
        HideAll(Vector3.zero);
        PoolManager.Instance.StartCoroutine(ReturnPool(explosion.gameObject, 4));
    }

    IEnumerator ReturnPool(GameObject gameObj, float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.ReleaseObject(gameObj);
    }

    void HideAll(Vector3 position)
    {
        KingBehaviour.Instance.coll.isTrigger = false;
        KingBehaviour.Instance.transform.DOMove(position, 2);
        KingBehaviour.Instance.transform.DOScale(Vector3.zero, 2);

        var shield = GameObject.FindGameObjectWithTag("Shield");
        shield.transform.DOMove(position, 2);
        shield.transform.DOScale(Vector3.zero, 2);

        PlayerBehaviour.Instance.transform.DOMove(position, 2);
        PlayerBehaviour.Instance.transform.DOScale(Vector3.zero, 2);
        PlayerBehaviour.Instance.enabled = false;

        SpectrumCircle.Instance.transform.DOMove(position, 2);
        SpectrumCircle.Instance.transform.DOScale(Vector3.zero, 2);

        AudioMeasure.Instance.GetComponent<AudioSource>().DOFade(0, 2);
    }
}
