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
// GA_FREE_Demo01 class
// - Animates all GUIAnimFREE elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA FREE - Demo01 (960x600px)" scene.
// ######################################################################

public class GA_FREE_Demo01 : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################
	
	#region Variables

	// Canvas
	public Canvas m_Canvas;

    // GUIAnimFREE objects of Middle button play
    public GUIAnimFREE m_MiddleCenter_A;
    public GUIAnimFREE m_MiddleCenter_B;

	// GUIAnimFREE objects of title text
	public GUIAnimFREE m_Title1;
				
	// Toggle state of TopLeft, BottomLeft and BottomLeft buttons
	bool m_BottomLeft_IsOn = false;
	
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

    private void OnGUI()
    {

    }

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    void Start ()
	{
		// MoveIn m_Title1 and m_Title2
		StartCoroutine(MoveInTitleGameObjects());

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

	// MoveIn m_Title1 and m_Title2
	IEnumerator MoveInTitleGameObjects()
	{
		yield return new WaitForSeconds(1.0f);

		// MoveIn m_Title1 and m_Title2
		m_Title1.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
		
		// MoveIn all primary buttons
		StartCoroutine(MoveInPrimaryButtons());
	}
	
	// MoveIn all primary buttons
	IEnumerator MoveInPrimaryButtons()
	{
		yield return new WaitForSeconds(1.0f);

		// MoveIn all primary buttons
        m_MiddleCenter_A.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);
        m_MiddleCenter_B.MoveIn(GUIAnimSystemFREE.eGUIMove.Self);

        // Enable all scene switch buttons
        StartCoroutine(EnableAllDemoButtons());
	}

	// MoveOut all primary buttons
	public void HideAllGUIs()
	{
        m_MiddleCenter_A.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
        m_MiddleCenter_B.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);

        // MoveOut m_Title1 and m_Title2
        StartCoroutine(HideTitleTextMeshes());
	}
	
	// MoveOut m_Title1 and m_Title2
	IEnumerator HideTitleTextMeshes()
	{
		yield return new WaitForSeconds(1.0f);

		// MoveOut m_Title1 and m_Title2
		m_Title1.MoveOut(GUIAnimSystemFREE.eGUIMove.Self);
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

	
	#endregion // Toggle Button
}