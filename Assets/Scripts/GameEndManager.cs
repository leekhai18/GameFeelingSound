using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
    #region Variables

    // Canvas
    public Canvas m_Canvas;

    // GUIAnimFREE object of Dialog
    public GUIAnimFREE m_Dialog;

    #endregion // Variables

    #region MonoBehaviour

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        if (enabled)
        {
            // Set GUIAnimSystemFREE.Instance.m_AutoAnimation to false in Awake() will let you control all GUI Animator elements in the scene via scripts.
            GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
        }
    }


    // Use this for initialization
    void Start()
    {
        // Disable all scene switch buttons
        GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);

        //
        StartCoroutine(ShowDialogEndGame());
    }

    IEnumerator ShowDialogEndGame()
    {
        yield return new WaitForSecondsRealtime(AudioManager.Instance.GetAudioSource.length);

        ShowDialog();
        ShowContentDialog();

        PoolManager.Instance.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    #endregion

#region handle
    public void ShowDialog()
    {
        // MoveIn Dialog
        StartCoroutine(DialogMoveIn());
        StartCoroutine(EnableAllDemoButtons());
    }

    // MoveOut m_Dialog
    public void HideAllGUIs()
    {
        // MoveOut m_Dialog
        m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
    }

    IEnumerator EnableAllDemoButtons()
    {
        yield return new WaitForSeconds(1.0f);

        // Enable all scene switch buttons
        GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
    }

    IEnumerator DialogMoveIn()
    {
        yield return new WaitForSeconds(1);

        m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperScreenEdge;
        m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;

        // MoveIn m_Dialog by position
        m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
    }
    #endregion


    #region ScoreShow
    public Text scoreCount;
    public Text newBest;
    public Image[] listStars;
    public Text nameSong;

    public void ShowContentDialog()
    {
        nameSong.text = AudioManager.Instance.GetAudioSource.name;
        StartCoroutine(AnimationCountScore(Convert.ToInt32(GameManager.Instance.scoreCount.text)));
    }

    IEnumerator AnimationCountScore(int score)
    {
        int best = PlayerPrefs.GetInt("BestScore" + AudioManager.Instance.GetIndexCurrentLv.ToString());

        if (score == 0)
        {
            scoreCount.text = "0";
        }
        else
        {
            for (int i = 0; i < score + 1; i += 5)
            {
                yield return new WaitForSecondsRealtime(5.0f / score);
                scoreCount.text = i.ToString();
            }

            if (score > best)
            {
                newBest.enabled = false;
            }
        }

        StartCoroutine(AnimationCountStar());
    }

    IEnumerator AnimationCountStar()
    {
        int numStar = 0;
        int numCollision = GameManager.Instance.numOfCollition;

        if (numCollision == 0)
            numStar = 3;
        else if (numCollision < 5)
            numStar = 2;
        else
            numStar = 1;

        //Save best stars foreach level
        SaveBestStars(numStar, AudioManager.Instance.GetIndexCurrentLv);

        for (int i = 0; i < numStar; i++)
        {
            yield return new WaitForSeconds(0.2f);
            listStars[i].enabled = true;
        }
    }

    void SaveBestStars(int numStar, int indexLv)
    {
        string key = "BestStars" + indexLv.ToString();

        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetInt(key) < numStar)
                PlayerPrefs.SetInt(key, numStar);
        }
        else
        {
            PlayerPrefs.SetInt(key, numStar);
        }

        PlayerPrefs.Save();
    }

    #endregion
}
