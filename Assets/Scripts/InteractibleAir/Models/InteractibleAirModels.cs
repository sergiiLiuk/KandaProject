using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractibleAirModels : MonoBehaviour
{
    AppManager appManager;

    public UnityEvent onSelectEvent;
    public UnityEvent thisObject;

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
        appManager = FindObjectOfType(typeof(AppManager)) as AppManager;

        thisObject.Invoke();
    }

    void OnSelect()
    {
        onSelectEvent.Invoke();
        OnClickAudioFeedback();
    }

    public void InitiateManipulationRecognizer()
    {
        appManager.InitiateManipulationRecognizer();
    }

    public void InitiateDefaultRecognizer()
    {
        appManager.InitiateDefaultRecognizer();
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
