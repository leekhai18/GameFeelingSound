using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : Singleton<LoadingManager>
{
    public Text loadText;
    public Image panel;

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void ShowDialog()
    {
        StartCoroutine(EnableOnPanel());
        GA_FREE_Demo02_Load.Instance.ShowDialog();
    }

    public void HideDialog()
    {
        panel.enabled = false;
        GA_FREE_Demo02_Load.Instance.HideAllGUIs();
    }

    IEnumerator EnableOnPanel()
    {
        yield return new WaitForSeconds(1);
        panel.enabled = true;
    }
}