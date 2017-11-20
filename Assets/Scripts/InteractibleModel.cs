using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleModel : MonoBehaviour
{

    void GazeExited()
    {

    }

    void GazeEntered()
    {

    }

    void OnSelect()
    {
        /*for (int i = 0; i < defaultMaterials.Length; i++)
        {
            defaultMaterials[i].SetFloat("_Highlight", .5f);
        }*/

        // Play the audioSource feedback when we gaze and select a hologram.
        switch (this.gameObject.name)
        {
            case "Cube":
                Debug.Log("Dev-> Cube Selected");


                break;

            case "Cylinder":
                Debug.Log("Dev-> Cylinder Selected");


                break;

            case "Sphere":
                Debug.Log("Dev-> Sphere Selected");


                break;

            default:
                break;

        }


    }
}
