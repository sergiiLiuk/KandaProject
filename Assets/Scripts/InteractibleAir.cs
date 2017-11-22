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

    void GazeEntered()
    {
        Debug.Log("TEst");
        Debug.Log("curr Object: " + this.gameObject.name);
    }

    void OnSelect()
    {
        /*for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetFloat("_Highlight", .5f);
        }*/

        switch (gameObject.name)
        {
            case "CubeModel":
                Debug.Log("Dev-> Cube Selected");
                this.SendMessage("CubeAction");
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

            case "BtnFixPosCube":
                Debug.Log("Dev-> Cube Placed");
                this.SendMessage("FixPosCube");
                OnClickAudioFeedback();
                break;

            case "BtnFixPosCylinder":
                Debug.Log("Dev-> Cylinder Placed");
                this.SendMessage("FixPoxCylinder");
                OnClickAudioFeedback();
                break;

            case "BtnFixPosSphere":
                Debug.Log("Dev-> Sphere Placed");
                this.SendMessage("FixPosSphere");
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
