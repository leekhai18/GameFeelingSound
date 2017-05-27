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
// GA_FREE_Demo02 class
// - Animates all GUIAnimFREE elements in the scene.
// - Responds to user mouse click or tap on buttons.
//
// Note this class is attached with "-SceneController-" object in "GA FREE - Demo02 (960x600px)" scene.
// ######################################################################

public class GA_FREE_Demo02_Load : Singleton<GA_FREE_Demo02_Load>
{

    // ########################################
    // Variables
    // ########################################

    #region Variables

    // Canvas
    public Canvas m_Canvas;

    // GUIAnimFREE object of Dialog
    public GUIAnimFREE m_Dialog;

    #endregion // Variables

    // ########################################
    // MonoBehaviour Functions
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    // ########################################

    #region MonoBehaviour

    // Awake is called when the script instance is being loaded.
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
    void Awake()
    {
        if (enabled)
        {
            // Set GUIAnimSystemFREE.Instance.m_AutoAnimation to false in Awake() will let you control all GUI Animator elements in the scene via scripts.
            GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
        }
    }

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    void Start()
    {

        // Disable all scene switch buttons
        // http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
        GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);
    }

    public void ShowDialog()
    {
        // MoveIn Dialog
        StartCoroutine(DialogMoveIn());
        StartCoroutine(EnableAllDemoButtons());
    }

    // Update is called every frame, if the MonoBehaviour is enabled.
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    void Update()
    {
    }

    #endregion // MonoBehaviour

    // ########################################
    // MoveIn/MoveOut functions
    // ########################################

    #region MoveIn/MoveOut

    // MoveOut m_Dialog
    public void HideAllGUIs()
    {
        // MoveOut m_Dialog
        m_Dialog.MoveOut(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
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
    IEnumerator DisableAllButtonsForSeconds(float DisableTime)
    {
        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);

        yield return new WaitForSeconds(DisableTime);

        // Enable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(true);
    }

    #endregion // Enable/Disable buttons

    // ########################################
    // Move dialog functions
    // ########################################

    #region Move Dialog

    // MoveIn m_Dialog by position
    IEnumerator DialogMoveIn()
    {
        yield return new WaitForSeconds(1);

        m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperScreenEdge;
        m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;

        // MoveIn m_Dialog by position
        m_Dialog.MoveIn(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
    }

    #endregion //  Move Dialog
}