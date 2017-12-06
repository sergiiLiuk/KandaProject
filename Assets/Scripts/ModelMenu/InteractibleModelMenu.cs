using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractibleModelMenu : MonoBehaviour
{
    #region Audio
    [Space]
    [Header("Audio Sources")]
    [Tooltip("Audio clip to play on object dismiss.")]
    public AudioClip OnClickFeedbackSound;
    #endregion

    ModelController ModelController;
    int nextModel = 1;

    public UnityEvent OnClickEvent;
    public UnityEvent OnStartEvent;
    public UnityEvent StopAllActionsEvent;
    public UnityEvent ManipulationGazeEvent;
    public UnityEvent ManipulationGestureEvent;
    public UnityEvent RotationEvent;
    public UnityEvent DefaultGesture;
    public UnityEvent NextModelEvent;

    // Use this for initialization
    void Start()
    {
        OnStartEvent.Invoke();
        ModelController = GameObject.FindObjectOfType(typeof(ModelController)) as ModelController;
    }

    void GazeExited()
    {
        SharedVariables.currentGestureState = SharedVariables.previousGestureState;
    }

    void GazeEntered()
    {
        if (gameObject.name == "BtnGazeMove" || gameObject.name == "BtnGestureMove" || gameObject.name == "BtnRotate" || gameObject.name == "BtnApprovePos")
        {
            SharedVariables.previousGestureState = SharedVariables.currentGestureState;
            SharedVariables.currentGestureState = 0;
            GestureManager.Instance.ResetGestureRecognizers();
        }
    }

    void OnSelect()
    {
        OnClickEvent.Invoke();
        StopAllActionsEvent.Invoke();
        ManipulationGazeEvent.Invoke();
        ManipulationGestureEvent.Invoke();
        DefaultGesture.Invoke();
        RotationEvent.Invoke();
        NextModelEvent.Invoke();
        OnClickAudioFeedback();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBtnClickedCol()
    {
        gameObject.GetComponent<Image>().color = Color.gray;
    }
    public void OnBtnNormCol()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public void StopAllActions()
    {
        SharedVariables.allowGestureRotation = false;
        SharedVariables.allowGazePlacing = false;
        SharedVariables.allowGesturePlacing = false;
    }

    // Manipulation Gaze 
    public void InitGazeManipulation()
    {
        SharedVariables.allowGazePlacing = true;
    }

    // Manipulation Gesture
    public void InitGestureManipulation()
    {
        SharedVariables.allowGesturePlacing = true;

        SharedVariables.currentGestureState = 1;
        SharedVariables.previousGestureState = SharedVariables.currentGestureState;
        GestureManager.Instance.ResetGestureRecognizers();
    }

    // Rotation
    public void InitRotationRecognizer()
    {
        SharedVariables.allowGestureRotation = true;

        SharedVariables.currentGestureState = 2;
        SharedVariables.previousGestureState = SharedVariables.currentGestureState;
        GestureManager.Instance.ResetGestureRecognizers();
    }

    // Default
    public void InitDefaultRecognizer()
    {
        SharedVariables.currentGestureState = 0;
        GestureManager.Instance.ResetGestureRecognizers();
    }

    // Next model
    public void InitNextModel()
    {
        if (nextModel < 3)
        {
            nextModel++;
            ModelController.ActivateNextModel(nextModel);
        }
        UIController.Instance.ModelMenu.SetActive(false);
    }

    void OnClickAudioFeedback()
    {
        if (OnClickFeedbackSound != null)
        {
            GameObject audioGameObject = new GameObject();
            audioGameObject.transform.position = gameObject.transform.position;
            AudioSource audioSource = audioGameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = audioGameObject.AddComponent<AudioSource>();
            }
            audioSource.clip = OnClickFeedbackSound;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;

            audioSource.Play();
        }
    }
}
