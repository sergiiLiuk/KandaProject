using Academy.HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractibleModel : MonoBehaviour
{

    public UnityEvent OnStartEvent;
    public UnityEvent OnClickEvent;

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
        OnStartEvent.Invoke();
    }

    void OnSelect()
    {
        OnClickEvent.Invoke();

        OnClickAudioFeedback();

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
