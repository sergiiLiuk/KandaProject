using Academy.HoloToolkit.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InteractibleAction performs custom actions when you gaze at the holograms.
/// </summary>
public class InteractibleAction : MonoBehaviour
{
    GameObject LoadSceneProgressBar;
    float progress;
    bool sceneBar = false;

    private Image LoadScenePrgressBarImg;
    private Text LoadScenePrgressBarText;

    void Start()
    {
        LoadSceneProgressBar = GameObject.Find("CanvasSceneLoadBar");
        LoadSceneProgressBar.SetActive(false);
    }

    void Update()
    {
        UpdateSceneLoadProgressBar();
    }
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


    void OnAirButton()
    {
        AppManager.appState = AppManager.APP_STATES.PLACING_MODE;
        LoadSceneProgressBar.SetActive(true);
        StartCoroutine(LoadAsyncScene("AirScene"));
    }

    private void UpdateSceneLoadProgressBar()
    {
        if (sceneBar)
        {
            LoadScenePrgressBarImg = GameObject.Find("LoadBarFillImg").GetComponent<Image>();
            LoadScenePrgressBarText = GameObject.Find("LoadBarFillTxt").GetComponent<Text>();
            LoadScenePrgressBarImg.fillAmount = progress;
            LoadScenePrgressBarText.text = (progress * 100) + "%";
        }
        if (progress > 0.9f)
        {
            LoadSceneProgressBar.SetActive(false);
            sceneBar = false;
        }
    }

    IEnumerator LoadAsyncScene(string SceneName)
    {
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.

        AsyncOperation asyncLoad = Application.LoadLevelAdditiveAsync(SceneName);
        sceneBar = true;
        //Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            // [0, 0.9] > [0, 1]
            progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            //Debug.Log("Loading progress: " + (progress * 100) + "%");
            /* if (asyncLoad.progress == 0.9f)
             {
                 Debug.Log("Scene Loaded");
             }*/
            yield return null;
        }
    }
}