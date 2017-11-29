using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractibleModel : MonoBehaviour
{
    GameObject ModelMenu;

    public UnityEvent NextEvent;
    public UnityEvent OnStartEvent;
    [Space]
    [Header("----------Model Menu----------")]
    public UnityEvent StopAllActionsEvent;
    public UnityEvent ManipulationGazeEvent;
    public UnityEvent ManipulationGestureEvent;
    public UnityEvent RotationEvent;
    public UnityEvent DefaultGesture;




    #region Audio
    [Space]
    [Header("Audio Sources")]
    [Tooltip("Audio clip to play on object dismiss.")]
    public AudioClip OnClickFeedbackSound;
    #endregion

    /*void GazeExited()
    {

    }

    void GazeEntered()
    {

    }*/

    void Start()
    {
        ModelMenu = GameObject.Find("Canvas(ModelMenu)");

        OnStartEvent.Invoke();
    }

    void OnSelect()
    {
        NextEvent.Invoke();

        StopAllActionsEvent.Invoke();
        ManipulationGestureEvent.Invoke();
        ManipulationGazeEvent.Invoke();
        RotationEvent.Invoke();
        DefaultGesture.Invoke();

        OnClickAudioFeedback();

    }

    // Update is called once per frame
    void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (SharedVariables.allowGazePlacing)
        {
            if (HandsManager.Instance.FocusedGameObject && HandsManager.Instance.FocusedGameObject.name == "Sea Tel 9711 Model")
            {
                ModelMenu.SetActive(false);
                SpatialMapping.Instance.DrawVisualMeshes = true;

                // Do a raycast into the world that will only hit the Spatial Mapping mesh.
                var headPosition = Camera.main.transform.position;
                var gazeDirection = Camera.main.transform.forward;

                RaycastHit hitInfo;
                if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                    30.0f, SpatialMapping.PhysicsRaycastMask))
                {
                    // Move this object's parent object to
                    // where the raycast hit the Spatial Mapping mesh.
                    this.transform.parent.position = hitInfo.point;
                    // Rotate this object's parent object to face the user.
                    Quaternion toQuat = Camera.main.transform.localRotation;
                    toQuat.x = 0;
                    toQuat.z = 0;
                    this.transform.parent.rotation = toQuat;
                }
            }
            else
            {
                ModelMenu.SetActive(true);
                SpatialMapping.Instance.DrawVisualMeshes = false;
            }
        }


        if (SharedVariables.allowGesturePlacing || SharedVariables.allowGestureRotation)
        {
            if (HandsManager.Instance.FocusedGameObject && HandsManager.Instance.FocusedGameObject.name == "Sea Tel 9711 Model")
            {
                ModelMenu.SetActive(false);
                SpatialMapping.Instance.DrawVisualMeshes = true;

            }
            else
            {
                ModelMenu.SetActive(true);
                SpatialMapping.Instance.DrawVisualMeshes = false;
            }
        }
    }

    // Manipulation Gaze 
    public void InitwGazeManipulationg()
    {
        SharedVariables.allowGazePlacing = true;
    }

    // Manipulation Gesture
    public void InitGestureManipulation()
    {
        SharedVariables.allowGesturePlacing = true;
        GestureManager.Instance.Transition(GestureManager.Instance.ManipulationRecognizer);
    }

    // Rotation
    public void InitRotationRecognizer()
    {
        SharedVariables.allowGestureRotation = true;
        GestureManager.Instance.Transition(GestureManager.Instance.NavigationRecognizer);
    }

    // Default
    public void InitDefaultRecognizer()
    {
        GestureManager.Instance.Transition(GestureManager.Instance.DefaultRecognizer);
    }

    public void StopAllActions()
    {
        SharedVariables.allowGestureRotation = false;
        SharedVariables.allowGazePlacing = false;
        SharedVariables.allowGesturePlacing = false;
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
