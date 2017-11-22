using Academy.HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// InteractibleManager keeps tracks of which GameObject
/// is currently in focus.
/// </summary>
public class InteractibleManager : Singleton<InteractibleManager>
{
    public GameObject FocusedGameObject { get; private set; }

    private GameObject oldFocusedGameObject = null;

    void Start()
    {
        FocusedGameObject = null;
    }

    void Update()
    {
        oldFocusedGameObject = FocusedGameObject;

        if (GazeManager.Instance.Hit)
        {
            RaycastHit hitInfo = GazeManager.Instance.HitInfo;
            if (hitInfo.collider != null)
            {
                FocusedGameObject = hitInfo.collider.gameObject;
            }
            else
            {
                FocusedGameObject = null;
            }
        }
        else
        {
            FocusedGameObject = null;
        }

        if (FocusedGameObject != oldFocusedGameObject)
        {
            ResetFocusedInteractible();

            if (FocusedGameObject != null)
            {

                if (FocusedGameObject.GetComponent<Interactible>() != null)
                {
                    FocusedGameObject.SendMessage("GazeEntered");
                }
                SceneManager.MergeScenes(SceneManager.GetSceneByName("OnAppLoadScene"), SceneManager.GetSceneByName("AirScene"));

                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("OnAppLoadScene"))
                {

                    Debug.Log("HEREEEEE");
                    if (oldFocusedGameObject.GetComponent<InteractibleAir>() != null)
                    {
                        oldFocusedGameObject.SendMessage("GazeEntered");
                    }
                }
            }
        }
    }

    private void ResetFocusedInteractible()
    {
        if (oldFocusedGameObject != null)
        {
            if (oldFocusedGameObject.GetComponent<Interactible>() != null)
            {
                oldFocusedGameObject.SendMessage("GazeExited");
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("AirScene"))
            {
                if (oldFocusedGameObject.GetComponent<InteractibleAir>() != null)
                {
                    oldFocusedGameObject.SendMessage("GazeExited");
                }
            }
        }
    }
}