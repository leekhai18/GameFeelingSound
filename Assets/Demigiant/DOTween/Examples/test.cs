using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour {

    public Camera cam;

	// Use this for initialization
	void Start () {
        
        cam.DOShakePosition(2).OnComplete(() =>
        {
            //cam.transform.position = Vector3.zero;
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
