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
// GA_FREE_Demo05 class
// - Animates all GUIAnimFREE elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA FREE - Demo05 (960x600px)" scene.
// ######################################################################

public class GA_FREE_Demo05 : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################
	
	#region Variables

	// Canvas
	public Canvas m_Canvas;
		
	// GUIAnimFREE objects of dialogs
	public GUIAnimFREE m_Dialog1;
	public GUIAnimFREE m_Dialog2;
	public GUIAnimFREE m_Dialog3;
	public GUIAnimFREE m_Dialog4;
	
	#endregion // Variables
	
	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################
	
	#region MonoBehaviour
	
	// Awake is called when the script instance is being loaded.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
	void Awake ()
	{
		if(enabled)
		{
			// Set GUIAnimSystemFREE.Instance.m_AutoAnimation to false in Awake() will let you control all GUI Animator elements in the scene via scripts.
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start ()
	{		
		// MoveIn m_Title1 and m_Title2
		StartCoroutine(MoveInPrimaryButtons());

		// Disable all scene switch buttons	
		// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update ()
	{		
	}
	
	#endregion // MonoBehaviour
	
	// ########################################
	// MoveIn/MoveOut functions
	// ########################################
	
	#region MoveIn/MoveOut	
	// MoveIn m_Dialog
	IEnumerator MoveInPrimaryButtons()
	{
		yield return new WaitForSeconds(1.0f);
		
		// MoveIn dialogs
		m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);	
		m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);	
		m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);	
		m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		
		// Enable all scene switch buttons
		StartCoroutine(EnableAllDemoButtons());
	}
	
	public void HideAllGUIs()
	{
		// MoveOut dialogs
		m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}	
	#endregion // MoveIn/MoveOut
	
	// ########################################
	// Enable/Disable button functions
	// ########################################
	
	#region Enable/Disable buttons
	
	// Enable/Disable all scene switch Coroutine	
	IEnumerator EnableAllDemoButtons()
	{
		yield return new WaitForSeconds(1.0f);

		// Enable all scene switch buttons
		// http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
	}
	
	// Disable all buttons for a few seconds
	IEnumerator DisableButtonForSeconds(GameObject GO, float DisableTime)
	{
		// Disable all buttons
		GUIAnimSystemFREE.Instance.EnableButton(GO.transform, false);
		
		yield return new WaitForSeconds(DisableTime);
		
		// Enable all buttons
		GUIAnimSystemFREE.Instance.EnableButton(GO.transform, true);
	}
	
	#endregion // Enable/Disable buttons
	
	// ########################################
	// UI Responder functions
	// ########################################
	
	#region UI Responder
	
	public void OnButton_Dialog1()
	{			
		// Remain m_Dialog1
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        StartCoroutine(GoToScene_Dialog1());
    }

    public void OnButton_Dialog2()
	{
        // Remain m_Dialog2
        m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        StartCoroutine(GoToScene_Dialog2());
    }

    public void OnButton_Dialog3()
	{
        // Remain m_Dialog3
        m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        StartCoroutine(GoToScene_Dialog3());
    }

    public void OnButton_Dialog4()
	{
        // Remain m_Dialog4
        m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        StartCoroutine(GoToScene_Dialog4());
    }

    public void OnButton_MoveOutAllDialogs()
	{
		// MoveOut dialogs
		m_Dialog1.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog2.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog3.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
		m_Dialog4.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);		
	}


    IEnumerator GoToScene_Dialog1()
    {
        yield return new WaitForSeconds(2);
        GA_FREE_OpenOtherScene.Instance.ButtonOpenSongScene();
    }
    IEnumerator GoToScene_Dialog2()
    {
        yield return new WaitForSeconds(2);
        GA_FREE_OpenOtherScene.Instance.ButtonOpenSongScene();
    }
    IEnumerator GoToScene_Dialog3()
    {
        yield return new WaitForSeconds(2);
        GA_FREE_OpenOtherScene.Instance.ButtonOpenSongScene();
    }
    IEnumerator GoToScene_Dialog4()
    {
        yield return new WaitForSeconds(2);
        GA_FREE_OpenOtherScene.Instance.ButtonOpenSongScene();
    }

    #endregion // UI Responder

    // ########################################
    // Move dialog functions
    // ########################################

    #region Move Dialog

    // MoveIn m_Dialog1
    IEnumerator Dialog1_MoveIn()
	{
		yield return new WaitForSeconds(1);
		
		// Reset children of m_Dialog1
		m_Dialog1.ResetAllChildren();
		
		// Moves m_Dialog1 back to screen to screen
		m_Dialog1.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}
	
	// MoveIn m_Dialog2
	IEnumerator Dialog2_MoveIn()
	{
		yield return new WaitForSeconds(1);
		
		// Reset children of m_Dialog2
		m_Dialog2.ResetAllChildren();
		
		// Moves m_Dialog2 back to screen to screen
		m_Dialog2.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}
	
	// MoveIn m_Dialog3
	IEnumerator Dialog3_MoveIn()
	{
		yield return new WaitForSeconds(1);
		
		// Reset children of m_Dialog3
		m_Dialog3.ResetAllChildren();
		
		// Moves m_Dialog3 back to screen to screen
		m_Dialog3.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}
	
	// MoveIn m_Dialog4
	IEnumerator Dialog4_MoveIn()
	{
		yield return new WaitForSeconds(1);
		
		// Reset children of m_Dialog4
		m_Dialog4.ResetAllChildren();
		
		// Moves m_Dialog4 back to screen to screen
		m_Dialog4.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
	}

    #endregion // Move Dialog


}
