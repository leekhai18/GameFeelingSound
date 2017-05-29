using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStarsManager : MonoBehaviour
{
    public Image[] listImage;
    public int level;
    bool isShow = true;

	// Use this for initialization
	void Start () {
        //ShowStar();
    }
	
	// Update is called once per frame
	void Update () {
        //if (isShow == false)
        //    ShowStar();
	}

    void ShowStar()
    {
        //string key = "BestStars" + lv.ToString();

        //for (int i = 0; i < 2; i++)
        //{
        //    listImage[i].gameObject.SetActive(true);
        //    listImage[i + 3].gameObject.SetActive(true);
        //}
    }
}
