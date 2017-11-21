using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleAir : MonoBehaviour
{
    #region Audio
    [Space]
    [Header("Audio Sources")]
    [Tooltip("Audio clip to play on object dismiss.")]
    public AudioClip OnClickFeedbackSound;
    #endregion 

    /*void GazeExited()
    {

    }*/

    /* void GazeEntered()
     {

     }*/

    void OnSelect()
    {
        /*for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetFloat("_Highlight", .5f);
        }*/

        switch (this.gameObject.name)
        {
            case "CubeModel":
                Debug.Log("Dev-> Cube Selected");
                this.SendMessageUpwards("CubeAction");
                OnClickAudioFeedback();
                break;

            case "CylinderModel":
                Debug.Log("Dev-> Cylinder Selected");
                this.SendMessageUpwards("CylinderAction");
                OnClickAudioFeedback();
                break;

            case "SphereModel":
                Debug.Log("Dev-> Sphere Selected");
                this.SendMessageUpwards("SphereAction");
                OnClickAudioFeedback();
                break;
            //--------------------------------------

            case "BtnPlaceCube":
                Debug.Log("Dev-> Cube Placed");
                this.SendMessageUpwards("PlaceCube");
                OnClickAudioFeedback();
                break;

            case "BtnPlaceCylinder":
                Debug.Log("Dev-> Cylinder Placed");
                //this.SendMessageUpwards("PlaceCylinder");
                OnClickAudioFeedback();
                break;

            case "BtnPlaceSphere":
                Debug.Log("Dev-> Sphere Placed");
                this.SendMessageUpwards("PlaceSphere");
                OnClickAudioFeedback();
                break;

            default:
                break;

        }

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
