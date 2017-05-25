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
	void Start () {		
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update () {		
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

    // Open Song Scene
    public void ButtonOpenSongScene()
    {
        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);

        // Waits 0 secs for Moving Out animation then load next level
        GUIAnimSystemFREE.Instance.LoadLevel("SpectrumCircle", 0);

        //gameObject.SendMessage("HideAllGUIs");
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
