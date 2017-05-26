// GUI Animator FREE
// Version: 1.1.5
// Compatilble: Unity 5.5.1 or higher, see more info in Readme.txt file.
//
// Developer:							Gold Experience Team (https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:4162)
//
// Unity Asset Store:					https://www.assetstore.unity3d.com/en/#!/content/58843
// See Full version:					https://www.assetstore.unity3d.com/en/#!/content/28709
//
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

#endregion // Namespaces

// ######################################################################
// GA_FREE_OpenOtherScene class
// This class handles 8 buttons for changing scene.
// ######################################################################

public class GA_FREE_OpenOtherScene : Singleton<GA_FREE_OpenOtherScene>
{

    // ########################################
    // MonoBehaviour Functions
    // ########################################

    #region MonoBehaviour

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    void Start() {
    }

    // Update is called every frame, if the MonoBehaviour is enabled.
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    void Update() {
    }

    #endregion // MonoBehaviour

    // ########################################
    // UI Responder functions
    // ########################################

    #region UI Responder
    // Open SelectLevel Scene
    public void ButtonOpenSelectLevelScene()
    {
        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);

        // Waits 0 secs for Moving Out animation then load next level
        GUIAnimSystemFREE.Instance.LoadLevel("SelectSong", 0);

        gameObject.SendMessage("HideAllGUIs");
    }

    // Open Song lv1 Scene
    public void ButtonOpenSongLv1()
    {
        // Select song source
        AudioManager.Instance.SelectSong(1);

        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);

        // Waits 0 secs for Moving Out animation then load next level
        GUIAnimSystemFREE.Instance.LoadLevel("SpectrumCircle", 0);

        //gameObject.SendMessage("HideAllGUIs");
    }

    // Open Song lv2 Scene
    public void ButtonOpenSongLv2()
    {
        // Select song source
        AudioManager.Instance.SelectSong(2);

        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);

        // Waits 0 secs for Moving Out animation then load next level
        GUIAnimSystemFREE.Instance.LoadLevel("SpectrumCircle", 0);

        //gameObject.SendMessage("HideAllGUIs");
    }

    // Open Song lv3 Scene
    public void ButtonOpenSongLv3()
    {
        // Select song source
        AudioManager.Instance.SelectSong(3);

        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);

        // Waits 0 secs for Moving Out animation then load next level
        GUIAnimSystemFREE.Instance.LoadLevel("SpectrumCircle", 0);

        //gameObject.SendMessage("HideAllGUIs");
    }


    // Open Song lv4 Scene
    public void ButtonOpenSongLv4()
    {
        // Select song source
        AudioManager.Instance.SelectSong(4);

        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);

        // Waits 0 secs for Moving Out animation then load next level
        GUIAnimSystemFREE.Instance.LoadLevel("SpectrumCircle", 0);

        //gameObject.SendMessage("HideAllGUIs");
    }

    // Load Current Scene
    public void LoadCurrentScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);
    }


    //Open Home Scene
    public void ButtonOpenHomeScene()
    {
        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);

        // Waits 0 secs for Moving Out animation then load next level
        GUIAnimSystemFREE.Instance.LoadLevel("Home", 0);
   
        gameObject.SendMessage("HideAllGUIs");
    }

    #endregion // UI Responder
}
