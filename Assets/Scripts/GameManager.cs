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

        KingBehaviour.Instance.transform.DOMove(blackHole.transform.position, 2);
        KingBehaviour.Instance.transform.DOScale(Vector3.zero, 2);

        var shield = GameObject.FindGameObjectWithTag("Shield");
        shield.transform.DOMove(blackHole.transform.position, 2);
        shield.transform.DOScale(Vector3.zero, 2);

        PlayerBehaviour.Instance.transform.DOMove(blackHole.transform.position, 2);
        PlayerBehaviour.Instance.transform.DOScale(Vector3.zero, 2);
        PlayerBehaviour.Instance.enabled = false;

        SpectrumCircle.Instance.transform.DOMove(blackHole.transform.position, 2);
        SpectrumCircle.Instance.transform.DOScale(Vector3.zero, 2);

        AudioMeasure.Instance.GetComponent<AudioSource>().DOFade(0, 2);
    }
}
