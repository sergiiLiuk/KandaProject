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

                if (SharedVariables.seaScene)
                {
                    if (FocusedGameObject.GetComponent<InteractibleModelMenu>() != null)
                    {
                        FocusedGameObject.SendMessage("GazeEntered");
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

            if (SharedVariables.seaScene)
            {
                if (oldFocusedGameObject.GetComponent<InteractibleModelMenu>() != null)
                {
                    oldFocusedGameObject.SendMessage("GazeExited");
                }
            }
        }
    }
}