using Academy.HoloToolkit.Unity;
using UnityEngine;

/// <summary>
/// InteractibleAction performs custom actions when you gaze at the holograms.
/// </summary>
public class InteractibleAction : MonoBehaviour
{
    public GameObject PerformText;
    /*[Tooltip("Drag the Tagalong prefab asset you want to display.")]
    public GameObject ObjectToTagAlong;

    void PerformTagAlong()
    {
        if (ObjectToTagAlong == null)
        {
            return;
        }

        // Recommend having only one tagalong.
        GameObject existingTagAlong = GameObject.FindGameObjectWithTag("TagAlong");
        if (existingTagAlong != null)
        {
            return;
        }

        GameObject instantiatedObjectToTagAlong = GameObject.Instantiate(ObjectToTagAlong);

        instantiatedObjectToTagAlong.SetActive(true);

        instantiatedObjectToTagAlong.AddComponent<Billboard>();

        instantiatedObjectToTagAlong.AddComponent<SimpleTagalong>();
   }*/

    void PerfomText()
    {
        if (!PerformText.activeSelf)
        {
            PerformText.SetActive(true);
        }
        else
        {
            PerformText.SetActive(false);
        }

    }
}